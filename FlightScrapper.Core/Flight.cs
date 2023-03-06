namespace FlightScrapper.Core
{
    public class Flight
    {
        public string OriginCity { get; private set; }
        public string OriginAirportCode { get; private set; }
        public string DestinationName { get; private set; }
        public string DestinationAirportCode { get; private set; }
        public DateTime Date { get; private set; }
        public decimal PriceInPln { get; private set; }
        public string AirlineName { get; private set; }

        public Flight(string originCity, string originAirportCode, string destinationName, string destinationAirportCode, DateTime date, decimal priceInPln, string airlineName)
        {
            OriginCity = originCity;
            OriginAirportCode = originAirportCode;
            DestinationName = destinationName;
            DestinationAirportCode = destinationAirportCode;
            Date = date;
            PriceInPln = priceInPln;
            AirlineName = airlineName;
        }
    }
}
