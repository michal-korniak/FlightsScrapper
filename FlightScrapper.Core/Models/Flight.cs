namespace FlightScrapper.Core.Models
{
    public class Flight
    {
        public string OriginCountry { get; init; }
        public string OriginCity { get; init; }
        public string OriginAirportCode { get; init; }
        public string DestinationCountry { get; init; }
        public string DestinationCity { get; init; }
        public string DestinationAirportCode { get; init; }
        public DateTime Date { get; init; }
        public decimal? PriceInPln { get; init; }
        public string AirlineName { get; init; }
    }
}