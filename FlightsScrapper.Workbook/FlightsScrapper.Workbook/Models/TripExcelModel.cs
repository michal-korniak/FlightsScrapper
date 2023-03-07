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

        [EpplusTableColumn(NumberFormat = "yyyy-MM-dd hh:mm")]
        public DateTime ArrivalFlightDate { get; }
        public DayOfWeek ArrivalFlightDayOfWeek { get; }

        [EpplusTableColumn(NumberFormat = "yyyy-MM-dd hh:mm")]
        public DateTime ReturnFlightDate { get; }
        public DayOfWeek ReturnFlightDayOfWeek { get; }

        [EpplusTableColumn(NumberFormat = "###0.00 PLN")]
        public decimal? ArrivalFlightPriceInPln { get; }

        [EpplusTableColumn(NumberFormat = "###0.00 PLN")]
        public decimal? ReturnFlightPriceInPln { get; }

        [EpplusTableColumn(NumberFormat = "###0.00 PLN")]
        public decimal? TotalPrice { get; }

        [EpplusTableColumn(NumberFormat = "###0.0")]
        public TimeSpan Length { get; }

        public string ArrivalFlightCompany { get; }
        public string ReturnFlightCompany { get; }

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
            ArrivalFlightCompany = trip.ArrivalFlight.AirlineName;
            ReturnFlightCompany = trip.ReturnFlight.AirlineName;
        }
    }
}
