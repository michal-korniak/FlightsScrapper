using OfficeOpenXml.Attributes;

namespace FlightScrapper.Core
{
    public class Flight
    {
        public string OriginCity { get; private set; }
        public string OriginAirportCode { get; private set; }
        public string DestinationCity { get; private set; }
        public string DestinationAirportCode { get; private set; }

        [EpplusTableColumn(NumberFormat = "yyyy-MM-dd hh:mm:ss")]
        public DateTime Date { get; private set; }

        public decimal? PriceInPln { get; private set; }
        public string AirlineName { get; private set; }

        public Flight(string originCity, string originAirportCode, string destinationCity,
            string destinationAirportCode, DateTime date, decimal? priceInPln, string airlineName)
        {
            OriginCity = originCity;
            OriginAirportCode = originAirportCode;
            DestinationCity = destinationCity;
            DestinationAirportCode = destinationAirportCode;
            Date = date;
            PriceInPln = priceInPln;
            AirlineName = airlineName;
        }

        public override string ToString()
        {
            string format = "{0,-40} {1,-40} {2,-40} {3,-40} {4,-40} {5,-40} {6,-40}";
            return
                String.Format(format, OriginCity, OriginAirportCode, DestinationCity, DestinationAirportCode,
                    Date.ToString(), PriceInPln, AirlineName);
        }
    }
}