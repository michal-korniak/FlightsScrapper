using FlightScrapper.Core;
using FlightScrapper.Ryanair;

namespace FlightScrapper.App;

public class Program
{

    public static DateRange StartDate = new(new DateTime(2023, 03, 17, 12, 0, 0), new DateTime(2023, 03, 18, 10, 0, 0));
    public static DateRange EndDate = new(new DateTime(2023, 03, 19, 16, 0, 0), new DateTime(2023, 03, 19, 21, 0, 0));
    public static List<AirportCode> AirportsCodes = new() { AirportCode.LUZ, AirportCode.KRK, AirportCode.WMI, AirportCode.WAW };

    public static async Task Main()
    {
        var ryanairFlightProvider = new RyanairFlightProvider();

        var allFlights = new List<Flight>();
        foreach (var airportCode in AirportsCodes)
        {
            var flightsForAirport = await ryanairFlightProvider.GetFlights(airportCode, StartDate, EndDate);
            allFlights.AddRange(flightsForAirport);
        }


    }
}