namespace FlightScrapper.Wizzair.Api.ResponseModels.Map
{
    public class City
    {
        public string Iata { get; set; }
        public double Longitude { get; set; }
        public string CurrencyCode { get; set; }
        public double Latitude { get; set; }
        public string ShortName { get; set; }
        public string CountryName { get; set; }
        public string CountryCode { get; set; }
        public List<Connection> Connections { get; set; }
        public List<string> Aliases { get; set; }
        public bool IsExcludedFromGeoLocation { get; set; }
        public int Rank { get; set; }
        public List<int> Categories { get; set; }
        public bool IsFakeStation { get; set; }
    }
}
