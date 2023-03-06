namespace FlightScrapper.Ryanair.Api.ResponseModels.Routes;

public class AirportDto
{
    public string Code { get; set; }
    public string Name { get; set; }
    public string SeoName { get; set; }
    public List<string> Aliases { get; set; }
    public bool? Base { get; set; }
    public CityDto City { get; set; }
    public RegionDto Region { get; set; }
    public CountryDto Country { get; set; }
    public CoordinatesDto Coordinates { get; set; }
    public string TimeZone { get; set; }
}
