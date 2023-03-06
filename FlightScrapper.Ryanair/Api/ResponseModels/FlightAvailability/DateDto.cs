namespace FlightScrapper.Ryanair.Api.ResponseModels.FlightAvailability;

public class DateDto
{
    public DateTime? DateOut { get; set; }
    public List<FlightDto> Flights { get; set; }
}

