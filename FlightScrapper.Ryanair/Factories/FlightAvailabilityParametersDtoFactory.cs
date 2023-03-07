using FlightScrapper.Core;
using FlightScrapper.Ryanair.Api.RequestModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightScrapper.Ryanair.Factories
{
    internal static class FlightAvailabilityParametersDtoFactory
    {
        public static FlightAvailabilityParametersDto TwoWayFlight(string originAirportCode, string destinationAirportCode, DateRange arrivalDateRange, DateRange returnDateRange)
        {
            return new()
            {
                DateIn = returnDateRange.StartDate.ToString("yyyy-MM-dd"),
                FlexDaysBeforeIn = 0,
                FlexDaysIn = (int)returnDateRange.DaysDiffrence.TotalDays,
                DateOut = arrivalDateRange.StartDate.ToString("yyyy-MM-dd"),
                FlexDaysBeforeOut = 0,
                FlexDaysOut = (int)arrivalDateRange.DaysDiffrence.TotalDays,
                Origin = originAirportCode,
                Destination = destinationAirportCode,
                RoundTrip = true,
                ToUs = "AGREED"
            };
        }

        public static FlightAvailabilityParametersDto OneWayFlight(string originAirportCode, string destinationAirportCode, DateRange dateRange)
        {
            return new()
            {
                DateOut = dateRange.StartDate.ToString("yyyy-MM-dd"),
                FlexDaysBeforeOut = 0,
                FlexDaysOut = (int)dateRange.DaysDiffrence.TotalDays,
                Origin = originAirportCode,
                Destination = destinationAirportCode,
                RoundTrip = false,
                ToUs = "AGREED"
            };
        }
    }
}
