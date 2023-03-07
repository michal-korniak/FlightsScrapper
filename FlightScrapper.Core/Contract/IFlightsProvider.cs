using FlightScrapper.Core.Models;

namespace FlightScrapper.Core.Contract
{
    public interface IFlightsProvider
    {
        Task<IEnumerable<Flight>> GetFlights(AirportCode airportCode, DateRange arrivalDateRange, DateRange returnDateRange);
    }
}
