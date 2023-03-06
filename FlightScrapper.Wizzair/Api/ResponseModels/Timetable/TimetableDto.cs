using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightScrapper.Wizzair.Api.ResponseModels.Timetable
{

    public class TimetableDto
    {
        public List<Flight> OutboundFlights { get; set; }
        public List<Flight> ReturnFlights { get; set; }
    }
}
