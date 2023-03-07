using FlightScrapper.Core;
using FlightScrapper.Ryanair.Api;
using FlightScrapper.Ryanair.Api.RequestModels;
using FlightScrapper.Ryanair.Api.ResponseModels.Routes;
using System.Net.Http.Json;
using System.Security.Cryptography;

namespace FlightScrapper.Ryanair
{
    public class RyanairFlightsProvider : IFlightsProvider
    {
       private const int MaxNumberOfDatsToBeRequested = 6;

        public async Task<IEnumerable<Flight>> GetFlights(AirportCode airportCode, DateRange startDateRange, DateRange endDateRange)
        {
            ValidateParameters(endDateRange, startDateRange);

            RyanairApiClient client = new();

            IEnumerable<string> availableDestinationsAirportsCodes = await GetAvailableDestinationsAirportsCodes(client, airportCode);
            List<Flight> flights = new();
            foreach (var destinationAirportCode in availableDestinationsAirportsCodes)
            {
                IEnumerable<Flight> fightsForDestination = await GetAvailableFlights(client, airportCode.ToString(), destinationAirportCode, startDateRange, endDateRange);
                flights.AddRange(fightsForDestination);
            }

            return flights;
        }

        private void ValidateParameters(DateRange dateInRange, DateRange dateOutRange)
        {
            ArgumentNullException.ThrowIfNull(dateInRange);
            if (dateInRange.DaysDiffrence.TotalDays > 6)
            {
                throw new ArgumentOutOfRangeException($"{nameof(dateInRange)}. Difference between two dates should not be more than 6 days.");
            }

            ArgumentNullException.ThrowIfNull(dateOutRange);
            if (dateInRange.DaysDiffrence.TotalDays > 6)
            {
                throw new ArgumentOutOfRangeException($"{nameof(dateOutRange)}. Difference between two dates should not be more than 6 days.");
            }
        }

        private async Task<IEnumerable<string>> GetAvailableDestinationsAirportsCodes(RyanairApiClient ryanairApiClient, AirportCode originAirportCode)
        {
            IEnumerable<RouteDto> routes = await ryanairApiClient.GetRoutes(originAirportCode.ToString());
            return routes.Select(x => x.ArrivalAirport.Code);
        }

        private async Task<IEnumerable<Flight>> GetAvailableFlights(RyanairApiClient ryanairApiClient, string originAirportCode, string destinationAirportCode, DateRange startDateRange, DateRange endDateRange)
        {
            IEnumerable<DateRange> startDateChunks = startDateRange.ChunkByDaysNumber(MaxNumberOfDatsToBeRequested);
            IEnumerable<DateRange> endDateChunks = endDateRange.ChunkByDaysNumber(MaxNumberOfDatsToBeRequested);

            

            FlightAvailabilityParametersDto flightAvailabilityParameters = new()
            {
                DateIn = endDateRange.StartDate.ToString("yyyy-MM-dd"),
                FlexDaysBeforeIn = 0,
                FlexDaysIn = (int)endDateRange.DaysDiffrence.TotalDays,
                DateOut = startDateRange.StartDate.ToString("yyyy-MM-dd"),
                FlexDaysBeforeOut = 0,
                FlexDaysOut = (int)startDateRange.DaysDiffrence.TotalDays,
                Origin = originAirportCode,
                Destination = destinationAirportCode,
                RoundTrip = true,
                ToUs = "AGREED"
            };
            var flightAvailability = await ryanairApiClient.GetFlightAvailability(flightAvailabilityParameters);
            var flights = flightAvailability.Trips.SelectMany(trip
                => trip.Dates.SelectMany(date
                => date.Flights.Where(flight => flight.RegularFare != null).Select(flight
                => new Flight(trip.OriginName, trip.Origin, trip.DestinationName, trip.Destination, flight.Time.First(), flight.RegularFare.Fares.SingleOrDefault(fare => fare.Type == "ADT").Amount.Value, "Ryanair"))));

            var filteredFlights = flights.Where(flight =>
            {
                if (flight.OriginAirportCode == originAirportCode)
                {
                    return startDateRange.Includes(flight.Date);
                }
                else
                {
                    return endDateRange.Includes(flight.Date);
                }
            });

            return filteredFlights;
        }
    }
}
