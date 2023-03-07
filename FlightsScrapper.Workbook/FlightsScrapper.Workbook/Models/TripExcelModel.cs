using FlightScrapper.Core.Models;
using OfficeOpenXml.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightsScrapper.Workbook.Models
{
    public class TripExcelModel
    {
        public string OriginCity { get; }
        public string DestinationCity { get; }

        [EpplusTableColumn(NumberFormat = "yyyy-MM-dd hh:mm:ss")]
        public DateTime ArrivalFlightDate { get; }
        public DayOfWeek ArrivalFlightDayOfWeek { get; }

        [EpplusTableColumn(NumberFormat = "yyyy-MM-dd hh:mm:ss")]
        public DateTime ReturnFlightDate { get; }
        public DayOfWeek ReturnFlightDayOfWeek { get; }
        public decimal? ArrivalFlightPriceInPln { get; }
        public decimal? ReturnFlightPriceInPln { get; }
        public decimal? TotalPrice { get; }
        public TimeSpan Length { get; }

        public TripExcelModel(Trip trip)
        {
            OriginCity = trip.ArrivalFlight.OriginCity;
            DestinationCity = trip.ArrivalFlight.DestinationCity;
            ArrivalFlightDate = trip.ArrivalFlight.Date;
            ArrivalFlightDayOfWeek = trip.ArrivalFlight.Date.DayOfWeek;
            ReturnFlightDate = trip.ReturnFlight.Date;
            ReturnFlightDayOfWeek = trip.ReturnFlight.Date.DayOfWeek;
            ArrivalFlightPriceInPln = trip.ArrivalFlight.PriceInPln;
            ReturnFlightPriceInPln = trip.ReturnFlight.PriceInPln;
            TotalPrice = trip.ArrivalFlight.PriceInPln + trip.ReturnFlight.PriceInPln;
            Length = ReturnFlightDate - ArrivalFlightDate;
        }
    }
}
