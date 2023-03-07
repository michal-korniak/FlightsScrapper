using FlightScrapper.Core.Models;

namespace FlightScrapper.Core.Services
{
    public static class TripsFactory
    {
        public static IEnumerable<Trip> CreateTrips(IEnumerable<AirportCode> originAirportsCodes, IEnumerable<Flight> flights)
        {
            Dictionary<string, List<Flight>> flightsByAirportCode = flights.GroupBy(flight => flight.OriginAirportCode).ToDictionary(x => x.Key, x => x.ToList());
            IEnumerable<Flight> flightsFromOriginAirports = flights.Where(flight => originAirportsCodes.Select(originAirport => originAirport.ToString()).Contains(flight.OriginAirportCode));

            foreach (Flight flight in flightsFromOriginAirports)
            {
                IEnumerable<Flight> returnFlights = flightsByAirportCode.GetValueOrDefault(flight.DestinationAirportCode)?
                    .Where(returnFlight => returnFlight.DestinationAirportCode==flight.OriginAirportCode && returnFlight.Date > flight.Date) ?? Enumerable.Empty<Flight>();
                foreach (var returnFlight in returnFlights)
                {
                    yield return new Trip(flight, returnFlight);
                }
            }
        }
    }
}
