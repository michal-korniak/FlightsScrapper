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

        public IEnumerable<DateRange> ChunkByDaysNumber(int daysNumber)
        {

            TimeSpan timeSpan = TimeSpan.FromDays(daysNumber);

            if ((EndDate - StartDate) > timeSpan)
            {
                DateRange currentChunk = new DateRange(StartDate, StartDate + timeSpan);
                yield return currentChunk;
                yield return new DateRange(currentChunk.EndDate, currentChunk.EndDate + timeSpan);


                TimeSpan timeLeft = (EndDate - StartDate)-

                while ((EndDate - currentChunk.StartDate) > timeSpan)
                {
                    currentChunk = new DateRange(currentChunk.EndDate, currentChunk.EndDate + timeSpan);
                    //yield return currentChunk;
                }
            }
            else
            {
                yield return this;
            }
        }
    }

}
