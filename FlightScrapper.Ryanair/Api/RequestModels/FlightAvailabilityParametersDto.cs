using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightScrapper.Ryanair.Api.RequestModels
{
    internal class FlightAvailabilityParametersDto
    {
        public string DateIn { get; set; }
        public string DateOut { get; set; }
        public string Origin { get; set; }
        public string Destination { get; set; }
        public int FlexDaysBeforeOut { get; set; }
        public int FlexDaysOut { get; set; }
        public int FlexDaysBeforeIn { get; set; }
        public int FlexDaysIn { get; set; }
        public bool RoundTrip { get; set; } = true;
        public string ToUs { get; set; } = "AGREED";
    }
}
