namespace FlightScrapper.Wizzair.Api.ResponseModels.Timetable
{
    public class Flight
    {
        public string DepartureStation { get; set; }
        public string ArrivalStation { get; set; }
        public DateTime DepartureDate { get; set; }
        public Price Price { get; set; }
        public string PriceType { get; set; }
        public List<DateTime> DepartureDates { get; set; }
        public string ClassOfService { get; set; }
        public bool HasMacFlight { get; set; }
    }
}
