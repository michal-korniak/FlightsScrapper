using FlightScrapper.Core;
using FlightScrapper.Ryanair.Api;
using FlightScrapper.Wizzair.Api.RequestModels.Timetable;
using FlightScrapper.Wizzair.Api.ResponseModels.Map;

namespace FlightScrapper.Wizzair
{
    public class WizzairFlightsProvider : IFlightsProvider
    {
        private readonly string _wizzairCookie;
        private string _wizzairRequestVerificationToken;

        public WizzairFlightsProvider(string wizzairCookie, string wizzairRequestVerificationToken)
        {
            _wizzairCookie = wizzairCookie;
            _wizzairRequestVerificationToken = wizzairRequestVerificationToken;
        }

        public async Task<IEnumerable<Flight>> GetFlights(AirportCode airportCode, DateRange arrivalDateRange, DateRange returnDateRange)
        {
            WizzairApiClient client = new(_wizzairCookie, _wizzairRequestVerificationToken);
            MapDto mapDto = await client.GetMap();

            if (!IsAirportSupported(mapDto, airportCode))
            {
                return Enumerable.Empty<Flight>();
            }

            Dictionary<string, string> cityNameByAirportCodeDict = CreateCityNameByAirportCodeDict(mapDto);
            IEnumerable<string> availableDestinationsAirportsCodes = GetAvailableDestinationsAirportsCodes(mapDto, airportCode);

            List<Flight> flights = new();
            foreach (var destinationAirportCode in availableDestinationsAirportsCodes)
            {
                try
                {
                    IEnumerable<Flight> fightsForDestination = await GetAvailableFlights(client, airportCode.ToString(), destinationAirportCode, arrivalDateRange, returnDateRange, cityNameByAirportCodeDict);
                    flights.AddRange(fightsForDestination);
                }
                catch (HttpRequestException ex)
                {
                    Console.WriteLine($"{nameof(WizzairFlightsProvider)}: Connection ignored, because of exception: {airportCode}->{destinationAirportCode}.");
                }

            }

            return flights;
        }

        private bool IsAirportSupported(MapDto mapDto, AirportCode airportCode)
        {
            return mapDto.Cities.Any(city => city.Iata == airportCode.ToString());
        }

        private Dictionary<string, string> CreateCityNameByAirportCodeDict(MapDto mapDto)
        {
            return mapDto.Cities.ToDictionary(city => city.Iata, city => city.ShortName.Trim());
        }

        private IEnumerable<string> GetAvailableDestinationsAirportsCodes(MapDto mapDto, AirportCode originAirportCode)
        {
            return mapDto.Cities.SingleOrDefault(city => city.Iata == originAirportCode.ToString()).Connections.Select(connection => connection.Iata);
        }

        private async Task<IEnumerable<Flight>> GetAvailableFlights(WizzairApiClient wizzairApiClient, string originAirportCode, string destinationAirportCode, DateRange arrivalDateRange, DateRange returnDateRange,
            Dictionary<string, string> cityNameByAirportCodeDict)
        {
            var timetableRequest = new TimetableRequestDto()
            {
                PriceType = "regular",
                AdultCount = 1,
                FlightList = new List<FlightRequestDto>() {
                    new FlightRequestDto()
                    {
                        DepartureStation=originAirportCode,
                        ArrivalStation=destinationAirportCode,
                        From=arrivalDateRange.StartDate.ToString("yyyy-MM-dd"),
                        To=arrivalDateRange.EndDate.ToString("yyyy-MM-dd")
                    },
                    new FlightRequestDto()
                    {
                        DepartureStation=destinationAirportCode,
                        ArrivalStation=originAirportCode,
                        From=returnDateRange.StartDate.ToString("yyyy-MM-dd"),
                        To=returnDateRange.EndDate.ToString("yyyy-MM-dd")
                    }
                }
            };
            var timetable = await wizzairApiClient.GetTimetable(timetableRequest);
            var flights = timetable.OutboundFlights.Union(timetable.ReturnFlights).Select(flight
                => new Flight(
                    cityNameByAirportCodeDict[flight.DepartureStation],
                    flight.DepartureStation,
                    cityNameByAirportCodeDict[flight.ArrivalStation],
                    flight.ArrivalStation,
                    flight.DepartureDates.First(),
                    flight.PriceType == "checkPrice" ? null : flight.Price.Amount,
                    "Wizzair")
                );

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
