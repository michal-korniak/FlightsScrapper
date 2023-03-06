namespace FlightScrapper.Ryanair.Api.ResponseModels.FlightAvailability;

public class TripDto
{
    public string Origin { get; set; }
    public string OriginName { get; set; }
    public string Destination { get; set; }
    public string DestinationName { get; set; }
    public string RouteGroup { get; set; }
    public string TripType { get; set; }
    public string UpgradeType { get; set; }
    public List<DateDto> Dates { get; set; }
}
