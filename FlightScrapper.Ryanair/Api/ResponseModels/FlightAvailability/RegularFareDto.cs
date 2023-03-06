namespace FlightScrapper.Ryanair.Api.ResponseModels.FlightAvailability;
public class RegularFareDto
{
    public string FareKey { get; set; }
    public string FareClass { get; set; }
    public List<FareDto> Fares { get; set; }
}
