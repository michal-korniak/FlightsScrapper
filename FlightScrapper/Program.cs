using FlightScrapper.App.Mocks;
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
    private readonly static int MaxTripLenghtInDays = 17;
    private readonly static DateRange ArrivalDateRange = new(DateTime.UtcNow.AddDays(1), DateTime.UtcNow.AddDays(61));
    private readonly static DateRange ReturnDateRange = new(DateTime.UtcNow.AddDays(1), DateTime.UtcNow.AddDays(61));
    private readonly static List<AirportCode> AirportsCodes = new() { AirportCode.KRK, AirportCode.WMI, AirportCode.WAW, AirportCode.RZE, AirportCode.LUZ };

    public static async Task Main()
    {
        Stopwatch stopwatch = Stopwatch.StartNew();
        IEnumerable<Flight> allFlights;

        HttpRequestMessage ryanairTemplateRequest = await RequestParser.ParseFromFile("RequestTemplates/Ryanair.nodefetch");
        HttpRequestMessage wizzairTemplateRequest = await RequestParser.ParseFromFile("RequestTemplates/Wizzair.nodefetch");
        IFlightsProvider wizzairFlightProvider = new WizzairFlightsProvider(wizzairTemplateRequest);
        IFlightsProvider ryanairFlightProvider = new RyanairFlightsProvider(ryanairTemplateRequest);

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