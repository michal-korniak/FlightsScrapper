using FlightScrapper.Core.Contract;
using FlightScrapper.Core.Extensions;
using FlightScrapper.Core.Models;
using FlightScrapper.Wizzair.Api;
using FlightScrapper.Wizzair.Api.RequestModels.Timetable;
using FlightScrapper.Wizzair.Api.ResponseModels.Map;
using FlightScrapper.Wizzair.Api.ResponseModels.Timetable;
using FlightScrapper.Wizzair.Factories;
using FlightScrapper.Wizzair.Models;

namespace FlightScrapper.Wizzair
{
    public class WizzairFlightsProvider : IFlightsProvider
    {
        private const int MaxNumberOfDatsToBeRequested = 30;
        private readonly HttpRequestMessage _requestTemplate;

        public WizzairFlightsProvider(HttpRequestMessage requestTemplate)
        {
            _requestTemplate = requestTemplate;
        }

        public async Task<IEnumerable<Flight>> GetFlights(AirportCode airportCode, DateRange arrivalDateRange, DateRange returnDateRange)
        {
            WizzairApiClient client = new(_requestTemplate);
            MapDto mapDto = await client.GetMap();

            if (!IsAirportSupported(mapDto, airportCode))
            {
                return Enumerable.Empty<Flight>();
            }

            Dictionary<string, AirportInfo> airportInfoByCodeDict = CreateAirportInfoByCodeDict(mapDto);
            IEnumerable<string> availableDestinationsAirportsCodes = GetAvailableDestinationsAirportsCodes(mapDto, airportCode);

            List<Flight> flights = new();
            foreach (var destinationAirportCode in availableDestinationsAirportsCodes)
            {
                Console.WriteLine($"Wizzair: Processing: {airportCode}->{destinationAirportCode}.");
                try
                {
                    IEnumerable<Flight> fightsForDestination = await GetAvailableFlights(client, airportCode.ToString(), destinationAirportCode, arrivalDateRange, returnDateRange, airportInfoByCodeDict);
                    flights.AddRange(fightsForDestination);
                }
                catch (DetailedHttpRequestException ex)
                {
                    if (ex.Message != "{\"validationCodes\":[\"InvalidMarket\"]}")
                    {
                        throw;
                    }
                }

            }

            return flights;
        }

        private bool IsAirportSupported(MapDto mapDto, AirportCode airportCode)
        {
            return mapDto.Cities.Any(city => city.Iata == airportCode.ToString());
        }

        private Dictionary<string, AirportInfo> CreateAirportInfoByCodeDict(MapDto mapDto)
        {
            return mapDto.Cities.ToDictionary(city => city.Iata, city => new AirportInfo()
            {
                City = city.ShortName,
                Country = city.CountryName,
                Code = city.Iata
            });
        }

        private IEnumerable<string> GetAvailableDestinationsAirportsCodes(MapDto mapDto, AirportCode originAirportCode)
        {
            return mapDto.Cities.SingleOrDefault(city => city.Iata == originAirportCode.ToString()).Connections.Select(connection => connection.Iata);
        }

        private async Task<IEnumerable<Flight>> GetAvailableFlights(WizzairApiClient wizzairApiClient, string originAirportCode, string destinationAirportCode, DateRange arrivalDateRange, DateRange returnDateRange,
            Dictionary<string, AirportInfo> airportInfoByCodeDict)
        {
            IEnumerable<DateRange> arrivalDateChunks = arrivalDateRange.ChunkByDaysNumber(MaxNumberOfDatsToBeRequested);
            IEnumerable<DateRange> returnDateChunks = returnDateRange.ChunkByDaysNumber(MaxNumberOfDatsToBeRequested);
            int leftArrivalDateChunks = arrivalDateChunks.Count();
            int leftReturnDateChunks = returnDateChunks.Count();

            List<TimetableDto> timetables = new();
            while (leftArrivalDateChunks > 0 || leftReturnDateChunks > 0)
            {
                TimetableRequestDto timetableRequest = null;
                if (leftArrivalDateChunks > 0 && leftReturnDateChunks > 0)
                {
                    var arrivalDateRangeForChunk = arrivalDateChunks.ElementAt(--leftArrivalDateChunks);
                    var returnDateRangeForChunk = returnDateChunks.ElementAt(--leftReturnDateChunks);
                    timetableRequest = TimetableRequestDtoFactory.TwoWayFlight(originAirportCode, destinationAirportCode, arrivalDateRangeForChunk, returnDateRangeForChunk);
                }
                else if (leftArrivalDateChunks > 0)
                {
                    var arrivalDateRangeForChunk = arrivalDateChunks.ElementAt(--leftArrivalDateChunks);
                    timetableRequest = TimetableRequestDtoFactory.OneWayFlight(originAirportCode, destinationAirportCode, arrivalDateRangeForChunk);
                }
                else if (leftReturnDateChunks > 0)
                {
                    var returnDateRangeForChunk = returnDateChunks.ElementAt(--leftReturnDateChunks);
                    timetableRequest = TimetableRequestDtoFactory.OneWayFlight(destinationAirportCode, originAirportCode, returnDateRangeForChunk);
                }
                TimetableDto timetable = await wizzairApiClient.GetTimetable(timetableRequest);
                timetables.Add(timetable);
            }

            var flights = timetables.SelectMany(timetable=>timetable.OutboundFlights.Union(timetable.ReturnFlights).Select(flight
                => new Flight()
                    {
                        AirlineName = "Wizzair",
                        OriginCountry = airportInfoByCodeDict[flight.DepartureStation].Country,
                        OriginCity = airportInfoByCodeDict[flight.DepartureStation].City,
                        OriginAirportCode = flight.DepartureStation,
                        DestinationCountry = airportInfoByCodeDict[flight.ArrivalStation].Country,
                        DestinationCity = airportInfoByCodeDict[flight.ArrivalStation].City,
                        DestinationAirportCode = flight.ArrivalStation,
                        Date = flight.DepartureDates.First(),
                        PriceInPln = flight.PriceType=="checkPrice" ? null : flight.Price.Amount
                    }
                ));

            var filteredFlights = flights.Where(flight =>
            {
                if ((flight.OriginAirportCode == originAirportCode && flight.DestinationAirportCode == destinationAirportCode) ||
                    (flight.OriginAirportCode == destinationAirportCode && flight.DestinationAirportCode == originAirportCode))
                {
                    if (flight.OriginAirportCode == originAirportCode)
                    {
                        return arrivalDateRange.Includes(flight.Date);
                    }
                    else
                    {
                        return returnDateRange.Includes(flight.Date);
                    }
                }
                return false;
            });

            return filteredFlights;
        }
    }
}
