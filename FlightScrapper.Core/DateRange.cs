using FlightScrapper.Core.Extensions;
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
            var daysTimeSpan = TimeSpan.FromDays(daysNumber);

            if (EndDate.Subtract(StartDate) < daysTimeSpan)
            {
                yield return this;
            }
            else
            {
                DateTime firstChunkStartDate = StartDate;
                DateTime firstChunkEndDate = (firstChunkStartDate + daysTimeSpan).GetLatestTimeOfPreviousDay();
                var lastChunk = new DateRange(firstChunkStartDate, firstChunkEndDate);
                yield return lastChunk;

                while (true)
                {
                    if (EndDate.Subtract(lastChunk.EndDate) < daysTimeSpan)
                    {
                        yield return new DateRange(lastChunk.EndDate.GetEearliesTimeOfNexDay(), EndDate);
                        break;
                    }
                    else
                    {
                        DateTime newChunkStartDate = lastChunk.EndDate.GetEearliesTimeOfNexDay();
                        DateTime newChunkEndDate = (newChunkStartDate + daysTimeSpan).GetLatestTimeOfPreviousDay();
                        lastChunk = new DateRange(newChunkStartDate, newChunkEndDate);
                        yield return lastChunk;
                    }
                }
            }
        }
    }

}
