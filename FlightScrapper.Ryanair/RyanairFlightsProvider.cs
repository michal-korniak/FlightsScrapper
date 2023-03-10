using FlightScrapper.Core.Contract;
using FlightScrapper.Core.Models;
using FlightScrapper.Ryanair.Api;
using FlightScrapper.Ryanair.Api.RequestModels;
using FlightScrapper.Ryanair.Api.ResponseModels.FlightAvailability;
using FlightScrapper.Ryanair.Api.ResponseModels.Routes;
using FlightScrapper.Ryanair.Factories;
using FlightScrapper.Ryanair.Models;

namespace FlightScrapper.Ryanair
{
    public class RyanairFlightsProvider : IFlightsProvider
    {
        private const int MaxNumberOfDatsToBeRequested = 6;

        public async Task<IEnumerable<Flight>> GetFlights(AirportCode airportCode, DateRange arrivalDateRange, DateRange returnDateRange)
        {
            RyanairApiClient client = new();

            Dictionary<string, AirportInfo> airportInfoByCodeDict = await CreateAirportInfoByCodeDict(client);
            IEnumerable<string> availableDestinationsAirportsCodes = await GetAvailableDestinationsAirportsCodes(client, airportCode);
            List<Flight> flights = new();
            foreach (var destinationAirportCode in availableDestinationsAirportsCodes)
            {
                Console.WriteLine($"Ryanair: Processing: {airportCode}->{destinationAirportCode}.");
                IEnumerable<Flight> fightsForDestination = await GetAvailableFlights(client, airportCode.ToString(), destinationAirportCode, arrivalDateRange, returnDateRange, airportInfoByCodeDict);
                flights.AddRange(fightsForDestination);
            }

            return flights;
        }

        private async Task<Dictionary<string, AirportInfo>> CreateAirportInfoByCodeDict(RyanairApiClient client)
        {
            IEnumerable<AirportDto> airports = await client.GetAirports();
            return airports.ToDictionary(airport => airport.Code, airport => new AirportInfo()
            {
                City = airport.City.Name,
                Code = airport.Code,
                Country = airport.Country.Name,
            });
        }

        private async Task<IEnumerable<string>> GetAvailableDestinationsAirportsCodes(RyanairApiClient ryanairApiClient, AirportCode originAirportCode)
        {
            IEnumerable<RouteDto> routes = await ryanairApiClient.GetRoutes(originAirportCode.ToString());
            return routes.Select(x => x.ArrivalAirport.Code);
        }

        private async Task<IEnumerable<Flight>> GetAvailableFlights(RyanairApiClient ryanairApiClient, string originAirportCode, string destinationAirportCode, DateRange arrivalDateRange, DateRange returnDateRange, Dictionary<string, AirportInfo> airportInfoByCodeDict)
        {
            IEnumerable<DateRange> arrivalDateChunks = arrivalDateRange.ChunkByDaysNumber(MaxNumberOfDatsToBeRequested);
            IEnumerable<DateRange> returnDateChunks = returnDateRange.ChunkByDaysNumber(MaxNumberOfDatsToBeRequested);
            int leftArrivalDateChunks = arrivalDateChunks.Count();
            int leftReturnDateChunks = returnDateChunks.Count();

            List<Task<FlightAvailabilityDto>> flightAvailabilitiesTasks = new();
            while (leftArrivalDateChunks > 0 || leftReturnDateChunks > 0)
            {
                FlightAvailabilityParametersDto flightAvailabilityParameters = null;
                if (leftArrivalDateChunks > 0 && leftReturnDateChunks > 0)
                {
                    var arrivalDateRangeForChunk = arrivalDateChunks.ElementAt(--leftArrivalDateChunks);
                    var returnDateRangeForChunk = returnDateChunks.ElementAt(--leftReturnDateChunks);
                    flightAvailabilityParameters = FlightAvailabilityParametersDtoFactory.TwoWayFlight(originAirportCode, destinationAirportCode, arrivalDateRangeForChunk, returnDateRangeForChunk);
                }
                else if (leftArrivalDateChunks > 0)
                {
                    var arrivalDateRangeForChunk = arrivalDateChunks.ElementAt(--leftArrivalDateChunks);
                    flightAvailabilityParameters = FlightAvailabilityParametersDtoFactory.OneWayFlight(originAirportCode, destinationAirportCode, arrivalDateRangeForChunk);
                }
                else if (leftReturnDateChunks > 0)
                {
                    var returnDateRangeForChunk = returnDateChunks.ElementAt(--leftReturnDateChunks);
                    flightAvailabilityParameters = FlightAvailabilityParametersDtoFactory.OneWayFlight(destinationAirportCode, originAirportCode, returnDateRangeForChunk);
                }
                var flightAvailabilityTask = ryanairApiClient.GetFlightAvailability(flightAvailabilityParameters);
                flightAvailabilitiesTasks.Add(flightAvailabilityTask);
            }

            FlightAvailabilityDto[] flightAvailabilities = await Task.WhenAll(flightAvailabilitiesTasks);

            var flights = flightAvailabilities.SelectMany(flightAvailability
                => flightAvailability.Trips.SelectMany(trip
                => trip.Dates.SelectMany(date
                => date.Flights.Where(flight => flight.RegularFare != null).Select(flight
                => new Flight()
                {
                    AirlineName = "Ryanair",
                    Date = flight.Time.First(),
                    OriginAirportCode = trip.Origin,
                    OriginCity = airportInfoByCodeDict[trip.Origin].City,
                    OriginCountry = airportInfoByCodeDict[trip.Origin].Country,
                    DestinationAirportCode = trip.Destination,
                    DestinationCity = airportInfoByCodeDict[trip.Destination].City,
                    DestinationCountry = airportInfoByCodeDict[trip.Destination].Country,
                    PriceInPln = flight.RegularFare.Fares.SingleOrDefault(fare => fare.Type == "ADT").Amount.Value
                }
                ))));

            var filteredFlights = flights.Where(flight =>
            {
                if (flight.OriginAirportCode == originAirportCode)
                {
                    return arrivalDateRange.Includes(flight.Date);
                }
                else
                {
                    return returnDateRange.Includes(flight.Date);
                }
            });

            return filteredFlights;
        }
    }
}
