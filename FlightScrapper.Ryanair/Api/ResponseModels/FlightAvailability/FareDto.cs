namespace FlightScrapper.Ryanair.Api.ResponseModels.FlightAvailability;
public class FareDto
{
    public string Type { get; set; }
    public decimal? Amount { get; set; }
    public int Count { get; set; }
    public bool? HasDiscount { get; set; }
    public double? PublishedFare { get; set; }
    public int DiscountInPercent { get; set; }
    public bool? HasPromoDiscount { get; set; }
    public double? DiscountAmount { get; set; }
    public bool? HasBogof { get; set; }
}
