using FlightScrapper.Core.Models;
using OfficeOpenXml.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightsScrapper.Workbook.Models
{
    public class FlightExcelModel
    {
        public string OriginCity { get; private set; }
        public string OriginAirportCode { get; private set; }
        public string DestinationCity { get; private set; }
        public string DestinationAirportCode { get; private set; }

        [EpplusTableColumn(NumberFormat = "yyyy-MM-dd hh:mm")]
        public DateTime Date { get; private set; }


        [EpplusTableColumn(NumberFormat = "###0.00 PLN")]
        public decimal? PriceInPln { get; private set; }

        public string AirlineName { get; private set; }

        public FlightExcelModel(Flight flight)
        {
            OriginCity = flight.OriginCity;
            OriginAirportCode = flight.OriginAirportCode;
            DestinationCity = flight.DestinationCity;
            DestinationAirportCode = flight.DestinationAirportCode;
            Date = flight.Date;
            PriceInPln = flight.PriceInPln;
            AirlineName = flight.AirlineName;
        }
    }
}
