using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightScrapper.Core
{

    public class DateRange
    {
        public DateRange(DateTime startDate, DateTime endDate)
        {
            if (endDate < startDate)
            {
                throw new ArgumentException("EndDate cannot be grater than startDate");
            }

            StartDate = startDate;
            EndDate = endDate;
        }

        public DateTime StartDate { get; private set; }
        public DateTime EndDate { get; private set; }

        public TimeSpan DaysDiffrence => EndDate.Date - StartDate.Date;

        public bool Includes(DateTime value)
        {
            return (StartDate <= value) && (value <= EndDate);
        }
    }

}
