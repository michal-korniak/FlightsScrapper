namespace FlightScrapper.Ryanair.Api.ResponseModels.FlightAvailability;
public class FlightDto
{
    public int FaresLeft { get; set; }
    public string FlightKey { get; set; }
    public int InfantsLeft { get; set; }
    public RegularFareDto RegularFare { get; set; }
    public string OperatedBy { get; set; }
    public List<SegmentDto> Segments { get; set; }
    public string FlightNumber { get; set; }
    public List<DateTime> Time { get; set; }
    public List<DateTime> TimeUTC { get; set; }
    public string Duration { get; set; }
}
