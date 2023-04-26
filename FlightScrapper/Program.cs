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
    //Wizzair API options
    //Should be changed before execution. Could be taken from any request that is send by wizzair.com to its API
    private readonly static string WizzairCookie = "ak_bmsc=80E4FC6268609007AA6502F65FB3F491~000000000000000000000000000000~YAAQhmReaOi4YZ2HAQAARZlXvRMl6AAO/fQddm1J51WpD3NLGxrADtLywHxIJKWr8YnHTq+7v+VXE/lt3uznHUejgk3QuplGQlKqV/d1PqNkb+C0n878wWbHNFjlf2g/liQ7aV4wWqCzgK165pjHVsqDiIbYkJsdtdt7k8cRiyTAkBYCQrxV7oS0VNssSoOaxNdGt/5i/RgWFV0rks4l1ZdMv22FGmDh3k6a+8/fV5UTE6BSMbaRQqGVLTluqwXvarPptjaV4J2FqiPlOukjpoaJoCO940GqoQiDTh95lbi5arBcj6/bdtqjr1tZMbv7HXDhAGrYI6ZLSANemzqdBt3WTrxPOb9gTK+JpmjajDqlZjPMYK6Cplm9ORAqOvA=; bm_sz=BAB6D193DC4ED88D60A06DCB18EB98E0~YAAQhmReaOq4YZ2HAQAARZlXvRMZ+J2MSnfegYyaQpJHfJK8gkvPcPntc2EktioIur8qCGmbhBzsu5kcB2ODXoY/yyE7ZHwhTHUDznS8q9nuPK9NZ0/jT4zA5Dpd5/krVENy4F9jTdlgciMSWe9m1uTWl6mq1K1Vksf08B+jGEey4V/gFCvYd7NW+UzQbx6cLm83BYbi9kJwOqecLWzpf/uo+Ny5Y7Pebj06Q0fHY2AqyR2gC2M7VG+QMKtmssTCdiTPr9OKBDdFAl/9+P+wslrILlX1DBW5JXLu3EW8zs74SsVyK4OWrq9XflZfJoD5L857MrSNHuDLdcl3Iw9h1LgWZLl85cFpAHZYh4lFxzJZXqlOgfJdI7R2+eKhvLLm+PEAv1hdHe2ZMuiWkpgcqD/zcQ==~3490101~3618113; ASP.NET_SessionId=emc3bvpvumjdi51ichmunb4k; RequestVerificationToken=5fac77fa800c4ec8bbc54f875afe81ab; bm_mi=B141149D63B7DD74EAB05697066A730A~YAAQhmReaMzYYZ2HAQAAy1tdvRPmW+R4wnY54hXr5vOGGkdQxLAlMcE3BViX18PaKh89TUGebKqXoproYYct+iV8ozvQ+4zz1sVK9qNpQlA7bUtE1BxMx0nx4nm1ARHlNHUNnhXeCHe8nyfMEqOQX8NFqneN3y1cVblblQq5yDn86NW71hqi9r0+RVfcn/hmJGjjZpDNUhwIWMOzpTULrM5nzPLbye6If8O8+yn9rbqCqdZnmdMR5EwhAq/7I0bApvzEZ9X1+6TK+4Mufo6Z1lvVUtYP2hrLYITwBz3A6DkD5/LMivXxpaTUC9l3NeQ=~1; _abck=944042F0FAD3E97178C799F8A4FB7ABE~-1~YAAQhmReaNLYYZ2HAQAApFxdvQkNlDLxxYcLZxb6u7d8+g+9mHMAC+qXU3ajc9E7RXBhI9Uzhz6fxX7cBz0L0lq+ljVNF55d7Tki7OPGH2ORj3Syi3wivxazNfKmYLsQPrOFY2Dz97VdSveqUGMV4LGaUQMr+4x7VHxW6reBpIMrqXbYkaYnm1TAqQsNRm2GZSnVb33TRWhQOdeiNCWTn4HF/9louBjq7w5G2AcSAQhju1Um7qOk//TodM0YqJcQMNEM5jPnWhrx+0oY15cp6RDtni3ewuRoV1whCXitziwHXOgEyB9BbuH4V7/yu/VPd0Fz+l4HWcLkKGEs9flFat8CZ2eknI+qCXF5EiC487AdWq+mEwR1NhgNUO5qhPkCiOTp5+ijYOwctiheGORgQC3AImGp4Tzd8v4DrX77IXbI4oKaGOmfAXDKsRp+gdPEb0qhV5p7TfJRPFfmcw==~-1~-1~-1; bm_sv=1C6135DCFB3ABC4C4B835BE1164F6985~YAAQF2ReaIqCL5yHAQAAWV5dvROZIB/f61A0/FOAeKXm3YtZ0qlsq9I9xzgBuZXGh4wscBDXxEXxEOtd9wJm34K/UfYn72gBoJ0Y1td/KqeOUzTVYRkz/dnvvlANq5Rw1bBO1CdgngRhjmqIexPY71y1WwOu5iTchxhDWUVogatE8SaF/1sFSBvZN3zS+sv3RwaFhri6/qA6GbzO6PtmMUmugKOu374fg4JWCn/EUmavMpp4JaOi19F5yuaSTi9cb/E=~1";
    private readonly static string WizzairRequestVerificationToken = "5fac77fa800c4ec8bbc54f875afe81ab";
    private readonly static string WizzairApiVersion = "17.0.0";

    //Search options
    private readonly static int MaxTripLenghtInDays = 7;
    private readonly static DateRange ArrivalDateRange = new(new DateTime(2023, 05, 5), new DateTime(2023, 10, 01));
    private readonly static DateRange ReturnDateRange = new(new DateTime(2023, 05, 5), new DateTime(2023, 10, 01));
    private readonly static List<AirportCode> AirportsCodes = new() { AirportCode.LUZ, AirportCode.KRK, AirportCode.WMI, AirportCode.WAW, AirportCode.RZE };

    //Debug options
    private readonly static string JsonFileName = "allFlights.json";
    private readonly static bool IsOfflineMode = false;     //set it false to take new data from APIs


    public static async Task Main()
    {
        Stopwatch stopwatch = Stopwatch.StartNew();
        IEnumerable<Flight> allFlights;
        if (IsOfflineMode && File.Exists(JsonFileName))
        {
            var jsonString = await File.ReadAllTextAsync(JsonFileName);
            allFlights = JsonSerializer.Deserialize<List<Flight>>(jsonString)!.OrderBy(x => x.PriceInPln).ToList();
        }
        else
        {
            WizzairFlightsProvider wizzairFlightProvider = new(WizzairCookie, WizzairRequestVerificationToken, WizzairApiVersion);
            RyanairFlightsProvider ryanairFlightProvider = new();

            Task<IEnumerable<Flight>> ryanairFlightsTask = GetFlightsForAirports(ryanairFlightProvider, AirportsCodes, ArrivalDateRange, ReturnDateRange);
            Task<IEnumerable<Flight>> wizzairFlightsTask = GetFlightsForAirports(wizzairFlightProvider, AirportsCodes, ArrivalDateRange, ReturnDateRange);

            using CancellationTokenSource cts = new();

            await TaskUtils.ExecuteTasksUntilFirstException(ryanairFlightsTask, wizzairFlightsTask);
            IEnumerable<Flight> ryanairFlights = await ryanairFlightsTask;
            IEnumerable<Flight> wizzairFlights = await wizzairFlightsTask;
            allFlights = ryanairFlights.Union(wizzairFlights);

            string jsonString = JsonSerializer.Serialize(allFlights.OrderBy(x => x.PriceInPln));
            await File.WriteAllTextAsync(JsonFileName, jsonString);
        }

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