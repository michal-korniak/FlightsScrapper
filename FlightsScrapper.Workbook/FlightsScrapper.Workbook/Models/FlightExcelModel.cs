using FlightScrapper.Core.Models;
using OfficeOpenXml.Attributes;

namespace FlightsScrapper.Workbook.Models
{
    public class FlightExcelModel
    {
        public string OriginCountry { get; }
        public string OriginCity { get; }
        public string OriginAirportCode { get; }
        public string DestinationCountry { get; }
        public string DestinationCity { get; }
        public string DestinationAirportCode { get; }

        [EpplusTableColumn(NumberFormat = "yyyy-MM-dd hh:mm")]
        public DateTime Date { get; }


        [EpplusTableColumn(NumberFormat = "#####0.00 zł")]
        public decimal? Price { get; }

        public string AirlineName { get; }

        public FlightExcelModel(Flight flight)
        {
            OriginCity = flight.OriginCity;
            OriginCountry = flight.OriginCountry;
            OriginAirportCode = flight.OriginAirportCode;
            DestinationCity = flight.DestinationCity;
            DestinationAirportCode = flight.DestinationAirportCode;
            DestinationCountry = flight.DestinationCountry;
            Date = flight.Date;
            Price = flight.PriceInPln;
            AirlineName = flight.AirlineName;
        }
    }
}
