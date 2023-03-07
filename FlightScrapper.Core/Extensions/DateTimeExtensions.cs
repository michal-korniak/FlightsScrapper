using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightScrapper.Core.Extensions
{
    internal static class DateTimeExtensions
    {
        internal static DateTime GetLatestTimeOfPreviousDay(this DateTime dateTime)
        {
            return new DateTime(dateTime.Year, dateTime.Month, dateTime.Day).AddTicks(-1);
        }

        internal static DateTime GetEearliesTimeOfNexDay(this DateTime dateTime)
        {
            return new DateTime(dateTime.Year, dateTime.Month, dateTime.Day+1);
        }
    }
}
