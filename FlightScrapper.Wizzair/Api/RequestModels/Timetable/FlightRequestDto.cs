namespace FlightScrapper.Wizzair.Api.RequestModels.Timetable
{
    public class FlightRequestDto
    {
        public string DepartureStation { get; set; }
        public string ArrivalStation { get; set; }
        public string From { get; set; }
        public string To { get; set; }
    }

}
