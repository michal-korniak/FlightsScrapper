using FlightScrapper.Core.Models;
using FlightScrapper.Core.Services;
using FlightsScrapper.Workbook;
using System.Diagnostics;
using System.Text.Json;

namespace FlightsScrapper.OfflineApp
{
    internal class Program
    {
        private readonly static List<AirportCode> AirportsCodes = new() { AirportCode.LUZ, AirportCode.KRK, AirportCode.WMI, AirportCode.WAW, AirportCode.RZE };
        private readonly static string JsonFileName = "allFlights.json";
        private readonly static int MaxTripLenghtInDays = 7;

        static async Task Main(string[] args)
        {
            var jsonString = await File.ReadAllTextAsync(JsonFileName);
            var allFlights = JsonSerializer.Deserialize<List<Flight>>(jsonString)!.OrderBy(x => x.PriceInPln).ToList();
            IEnumerable<Trip> trips = TripsFactory.CreateTrips(AirportsCodes, allFlights, MaxTripLenghtInDays);
            ExcelWriter.Write(allFlights, trips);
            Console.ReadKey();
        }
    }
}