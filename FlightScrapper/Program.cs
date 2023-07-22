using FlightScrapper.Core.Contract;
using FlightScrapper.Core.Models;
using FlightScrapper.Core.Services;
using FlightScrapper.Ryanair;
using FlightScrapper.Wizzair;
using FlightsScrapper.Workbook;
using System.Diagnostics;
using System.Text.Json;
using Flight = FlightScrapper.Core.Models.Flight;

namespace FlightScrapper.App;

public class Program
{
    private readonly static int MaxTripLenghtInDays = 7;
    private readonly static DateRange ArrivalDateRange = new(DateTime.UtcNow.AddDays(1), DateTime.UtcNow.AddDays(40));
    private readonly static DateRange ReturnDateRange = new(DateTime.UtcNow.AddDays(1), DateTime.UtcNow.AddDays(40));
    private readonly static List<AirportCode> AirportsCodes = new() { AirportCode.LUZ, AirportCode.KRK, AirportCode.WMI, AirportCode.WAW, AirportCode.RZE };

    public static async Task Main()
    {
        Stopwatch stopwatch = Stopwatch.StartNew();
        IEnumerable<Flight> allFlights;

        HttpRequestMessage wizzairTemplateRequest = await RequestParser.ParseFromFile("RequestTemplates/Wizzair.nodefetch");
        HttpRequestMessage ryanairTemplateRequest = await RequestParser.ParseFromFile("RequestTemplates/Ryanair.nodefetch");
        WizzairFlightsProvider wizzairFlightProvider = new(wizzairTemplateRequest);
        RyanairFlightsProvider ryanairFlightProvider = new(ryanairTemplateRequest);

        Task<IEnumerable<Flight>> ryanairFlightsTask = GetFlightsForAirports(ryanairFlightProvider, AirportsCodes, ArrivalDateRange, ReturnDateRange);
        Task<IEnumerable<Flight>> wizzairFlightsTask = GetFlightsForAirports(wizzairFlightProvider, AirportsCodes, ArrivalDateRange, ReturnDateRange);

        using CancellationTokenSource cts = new();

        await TaskUtils.ExecuteTasksUntilFirstException(ryanairFlightsTask, wizzairFlightsTask);
        IEnumerable<Flight> ryanairFlights = await ryanairFlightsTask;
        IEnumerable<Flight> wizzairFlights = await wizzairFlightsTask;
        allFlights = ryanairFlights.Union(wizzairFlights);


        IEnumerable<Trip> trips = TripsFactory.CreateTrips(AirportsCodes, allFlights, MaxTripLenghtInDays);
        ExcelWriter.Write(allFlights, trips);
        Console.WriteLine($"Processed in {stopwatch.Elapsed}");
        Console.ReadKey();
    }

    private static async Task<IEnumerable<Flight>> GetFlightsForAirports(IFlightsProvider flightsProvider, List<AirportCode> airportsCodes, DateRange arrivalDateRange, DateRange returnDateRange)
    {
        List<Flight> allFlights = new();
        foreach (var airportCode in airportsCodes)
        {
            IEnumerable<Flight> flightsForAirport = await flightsProvider.GetFlights(airportCode, arrivalDateRange, returnDateRange);
            allFlights.AddRange(flightsForAirport);
        }
        return allFlights;
    }
}