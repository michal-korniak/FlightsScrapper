namespace FlightScrapper.Ryanair.Api.ResponseModels.Routes;

public class RouteDto
{
    public AirportDto ArrivalAirport { get; set; }
    public bool? Recent { get; set; }
    public bool? Seasonal { get; set; }
    public string Operator { get; set; }
    public List<string> Tags { get; set; }
}