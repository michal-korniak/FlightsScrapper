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
        public string OriginCountry { get; }
        public string OriginCity { get; }
        public string DestinationCountry { get; }
        public string DestinationCity { get; }

        [EpplusTableColumn(NumberFormat = "yyyy-MM-dd hh:mm")]
        public DateTime ArrivalFlightDate { get; }
        public DayOfWeek ArrivalFlightDayOfWeek { get; }

        [EpplusTableColumn(NumberFormat = "yyyy-MM-dd hh:mm")]
        public DateTime ReturnFlightDate { get; }
        public DayOfWeek ReturnFlightDayOfWeek { get; }

        [EpplusTableColumn(NumberFormat = "#####0.00 zł")]
        public decimal? ArrivalFlightPrice { get; }

        [EpplusTableColumn(NumberFormat = "#####0.00 zł")]
        public decimal? ReturnFlightPrice { get; }

        [EpplusTableColumn(NumberFormat = "#####0.00 zł")]
        public decimal? TotalPrice { get; }

        [EpplusTableColumn(NumberFormat = "###0.0")]
        public double LengthInDays { get; }

        public string ArrivalFlightCompany { get; }
        public string ReturnFlightCompany { get; }

        public TripExcelModel(Trip trip)
        {
            OriginCountry = trip.ArrivalFlight.OriginCountry;
            OriginCity = trip.ArrivalFlight.OriginCity;
            DestinationCountry = trip.ReturnFlight.OriginCountry;
            DestinationCity = trip.ArrivalFlight.DestinationCity;
            ArrivalFlightDate = trip.ArrivalFlight.Date;
            ArrivalFlightDayOfWeek = trip.ArrivalFlight.Date.DayOfWeek;
            ReturnFlightDate = trip.ReturnFlight.Date;
            ReturnFlightDayOfWeek = trip.ReturnFlight.Date.DayOfWeek;
            ArrivalFlightPrice = trip.ArrivalFlight.PriceInPln;
            ReturnFlightPrice = trip.ReturnFlight.PriceInPln;
            TotalPrice = trip.ArrivalFlight.PriceInPln + trip.ReturnFlight.PriceInPln;
            LengthInDays = (ReturnFlightDate - ArrivalFlightDate).TotalDays;
            ArrivalFlightCompany = trip.ArrivalFlight.AirlineName;
            ReturnFlightCompany = trip.ReturnFlight.AirlineName;
        }
    }
}
