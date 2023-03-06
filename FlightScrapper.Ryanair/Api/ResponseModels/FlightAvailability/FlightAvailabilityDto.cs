namespace FlightScrapper.Ryanair.Api.ResponseModels.FlightAvailability;
public class FlightAvailabilityDto
{
    public string TermsOfUse { get; set; }
    public string Currency { get; set; }
    public int CurrPrecision { get; set; }
    public string RouteGroup { get; set; }
    public string TripType { get; set; }
    public string UpgradeType { get; set; }
    public List<TripDto> Trips { get; set; }
}