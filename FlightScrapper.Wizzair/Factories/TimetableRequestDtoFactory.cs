using FlightScrapper.Core.Models;
using FlightScrapper.Wizzair.Api.RequestModels.Timetable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightScrapper.Wizzair.Factories
{
    internal static class TimetableRequestDtoFactory
    {
        public static TimetableRequestDto TwoWayFlight(string originAirportCode, string destinationAirportCode, DateRange arrivalDateRange, DateRange returnDateRange)
        {
            return new TimetableRequestDto()
            {
                PriceType = "regular",
                AdultCount = 1,
                FlightList = new List<FlightRequestDto>() {
                    new FlightRequestDto()
                    {
                        DepartureStation=originAirportCode,
                        ArrivalStation=destinationAirportCode,
                        From=arrivalDateRange.StartDate.ToString("yyyy-MM-dd"),
                        To=arrivalDateRange.EndDate.ToString("yyyy-MM-dd")
                    },
                    new FlightRequestDto()
                    {
                        DepartureStation=destinationAirportCode,
                        ArrivalStation=originAirportCode,
                        From=returnDateRange.StartDate.ToString("yyyy-MM-dd"),
                        To=returnDateRange.EndDate.ToString("yyyy-MM-dd")
                    }
                }
            };

        }

        public static TimetableRequestDto OneWayFlight(string originAirportCode, string destinationAirportCode, DateRange dateRange)
        {
            return new TimetableRequestDto()
            {
                PriceType = "regular",
                AdultCount = 1,
                FlightList = new List<FlightRequestDto>() {
                    new FlightRequestDto()
                    {
                        DepartureStation=originAirportCode,
                        ArrivalStation=destinationAirportCode,
                        From=dateRange.StartDate.ToString("yyyy-MM-dd"),
                        To=dateRange.EndDate.ToString("yyyy-MM-dd")
                    },
                }
            };
        }
    }
}
