using FlightScrapper.Core.Contract;
using FlightScrapper.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightScrapper.App.Mocks
{
    internal class DummyFlightsProvider : IFlightsProvider
    {

        public DummyFlightsProvider(HttpRequestMessage wizzairTemplateRequest)
        {
        }

        public Task<IEnumerable<Flight>> GetFlights(AirportCode airportCode, DateRange arrivalDateRange, DateRange returnDateRange)
        {
            return Task.FromResult(Enumerable.Empty<Flight>());
        }
    }
}
