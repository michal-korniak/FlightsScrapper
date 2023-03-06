namespace FlightScrapper.Core
{
    public interface IFlightProvider
    {
        Task<IEnumerable<Flight>> GetFlights(AirportCode airportCode, DateRange dateInRange, DateRange dateOutRange);
    }
}
