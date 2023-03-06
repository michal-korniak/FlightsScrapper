using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightScrapper.Wizzair.Api.RequestModels.Timetable
{
    public class TimetableRequestDto
    {
        public List<FlightRequestDto> FlightList { get; set; }
        public string PriceType { get; set; }
        public int AdultCount { get; set; }
        public int ChildCount { get; set; }
        public int InfantCount { get; set; }
    }

}
