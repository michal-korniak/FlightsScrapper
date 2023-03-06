namespace FlightScrapper.Core
{
    public interface IFlightsProvider
    {
        Task<IEnumerable<Flight>> GetFlights(AirportCode airportCode, DateRange startDateRange, DateRange endDateRange);
    }
}
