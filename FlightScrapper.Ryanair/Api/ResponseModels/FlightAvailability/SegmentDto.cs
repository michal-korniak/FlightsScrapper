namespace FlightScrapper.Ryanair.Api.ResponseModels.FlightAvailability;
public class SegmentDto
{
    public int SegmentNr { get; set; }
    public string Origin { get; set; }
    public string Destination { get; set; }
    public string FlightNumber { get; set; }
    public List<DateTime> Time { get; set; }
    public List<DateTime> TimeUTC { get; set; }
    public string Duration { get; set; }
}
