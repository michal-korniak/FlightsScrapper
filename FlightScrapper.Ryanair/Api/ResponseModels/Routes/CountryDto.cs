namespace FlightScrapper.Ryanair.Api.ResponseModels.Routes;
public class CountryDto
{
    public string Code { get; set; }
    public string Iso3Code { get; set; }
    public string Name { get; set; }
    public string Currency { get; set; }
    public string DefaultAirportCode { get; set; }
    public bool? Schengen { get; set; }
}
