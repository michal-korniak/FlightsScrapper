using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightScrapper.Core.Models
{
    public class Trip
    {
        public Flight ArrivalFlight { get; }
        public Flight ReturnFlight { get; }

        public Trip(Flight arrivalFlight, Flight returnFlight)
        {
            ArrivalFlight = arrivalFlight;
            ReturnFlight = returnFlight;
        }
    }
}
