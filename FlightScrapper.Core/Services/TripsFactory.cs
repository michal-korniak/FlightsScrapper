using FlightScrapper.Core.Models;

namespace FlightScrapper.Core.Services
{
    public static class TripsFactory
    {
        public static IEnumerable<Trip> CreateTrips(IEnumerable<AirportCode> originAirportsCodes, IEnumerable<Flight> flights, int maxTripLenghtInDays)
        {
            Dictionary<string, List<Flight>> flightsByAirportCode = flights.GroupBy(flight => flight.OriginAirportCode).ToDictionary(x => x.Key, x => x.ToList());
            IEnumerable<Flight> flightsFromOriginAirports = flights.Where(flight => originAirportsCodes.Select(originAirport => originAirport.ToString()).Contains(flight.OriginAirportCode));

            foreach (Flight flight in flightsFromOriginAirports)
            {
                IEnumerable<Flight> flightsFromAirportCode = flightsByAirportCode.GetValueOrDefault(flight.DestinationAirportCode) ?? Enumerable.Empty<Flight>();
                IEnumerable<Flight> returnFlights = flightsFromAirportCode.Where(returnFlight
                    => returnFlight.DestinationAirportCode == flight.OriginAirportCode
                    && returnFlight.Date > flight.Date
                    && returnFlight.Date.Subtract(flight.Date) < TimeSpan.FromDays(maxTripLenghtInDays));
                foreach (var returnFlight in returnFlights)
                {
                    yield return new Trip(flight, returnFlight);
                }
            }
        }
    }
}
