namespace FlightScrapper.Wizzair.Api.ResponseModels.Map
{
    public class Connection
    {
        public string Iata { get; set; }
        public DateTime OperationStartDate { get; set; }
        public DateTime RescueEndDate { get; set; }
        public bool IsDomestic { get; set; }
        public bool IsNew { get; set; }
        public bool IsConnected { get; set; }
    }
}
