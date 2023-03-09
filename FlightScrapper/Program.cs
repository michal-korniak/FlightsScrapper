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
    private readonly static string WizzairCookie = "ak_bmsc=C90D723A7C2084D0298FD2E363D99158~000000000000000000000000000000~YAAQtbRmUj+1EL6GAQAAlmHBxhOIKjNbous+HMYVnQibUXrXHtFFOd9R1yN64rYrtqri4jRWBeE7duR4QA/fhi95AhNfe5CpDyWSOuWN23UPZefvePtga5bqc5wVVsmyoqUx4dOealCozIVdT5wVHOkh/ByOGG5elvvbG1wA6slA3dmHmF3aoTN+2vnty4g8N1PLKPje+8IeyouhMHHiBhXCGGwlIiZg6Hg5Mt6OaMmW/rAxOlxPZAueVHsLxBiFS2Xh6n89cfWnEimhj0IhQ8Z7FIfz0OgNttMTGZE5n7RHCdOuyCZnAaAoXuix8TMrkl6iTPypbzuQ29xU00RW231XuLgzy/2OqEqmmleDjliGzGPrY/P3v/Y6pT9qHgmxxvxvLwQrSl2egMF95A==; bm_sz=942B2314F2655E769BCB617CF73BC6B7~YAAQtbRmUkG1EL6GAQAAlmHBxhOFru/9W1Z08Im1quI3QKHuc/AJAq4TbENNWlZqUBC/ASfB5C0yD2/2txaY0NY2M5pPaNQDb6gREFpf8Nd060+Fyp4oa0V96fE9fOlGd71EQXbNME/SSH8ZrCuntMiIAw3UxdM0dJyfV3L1Wn+uN/8bGQyHob6X+PTZeFs7CJ4dI4uNQN8+k8etwwPrY6aiXzCftci1bj7bnh7ZhcmHOvQONYcaug2EO4GS3/aFSdTrnXJdbS8wmwxPOdFZwZ4C5Q+2z0Kzq2Uj9C5MS35RBaC/OQKqK34Q7IubdEvy5IbLHFbPKLP4wEhXaqpeFMg4RZPk+x8p3HgaQip2GA0c5em0z5NUqOSUiMbDxosMQr6/WDx1kk72/esbbZzPefmIww==~3487795~4342833; ASP.NET_SessionId=xmhgo1bk3dnuiyfw3pslxkfq; RequestVerificationToken=b8c5e0f4bf3d4adea634adb74cda3cb1; bm_mi=51F71D91DAEA0135F8905615282EC829~YAAQtbRmUlUSEb6GAQAApAPWxhMMKTbJM1gTsS9JlBRB8p+RuEwF/tXZo0d68TO1icZBrWlcqMy7tKU2ozzFt0bZa8u0tSnED4Xj6N1bNOjjAxMs8J9yN/H1F9xqNsP4U3mQcErv9rMQyDj+ZwSKS8UEcSy6SC3fstFjkCmWE78toroa52CdS6Hct/znhKEd8oF6qxfsLZcI5Uvld3gp0qjQFJuympKPFK3gGCYEyLfuMml2s4o3VCDl3X112FXw+Km0MoxS1K/HrS+EmPZ2/KW6LNludqyJ3tLpFFJGOgUtxXsvqYEZsmH47RIU4WCFQa5AjVef80pngsyWT2blauzGKvoT60QqT+ZO2YauTtV/j9EvDJj7u7Lv7zVA3uTVJR3Vdetm0z1Tg7eoYRs/SUbZjw==~1; _abck=944042F0FAD3E97178C799F8A4FB7ABE~-1~YAAQtbRmUloSEb6GAQAA5gXWxgl9TYVTo6WCPN7SNn1tRHE+E/fo6N88naXbsyQbmu2jcBxfmPB1ZUUv2DeHXB64SNnfOCDBAshfe+vwDy5TqW1kD6xZMm2pK90gjKUIVSYM7LAPYAb7pVLgLR38hDGy/nCBwj5aRkfSOTXixHjq9whRzwsv7LPTxI+VndmC91G269xUUtwYOtXcapBdfUUbmnBrs1hHrVlHL7N3bWFrOL6+c9bE8rpiLXkcu7CzJOQ/2yHBTCyHxqgg97eRbt8aBei3WErDbcfXik/8ibe7ES2IIy9d3P0FaYA/KpSQe60gCsM8Di3P2L/yy3roMJ+wg/CgZQ6ERDkLnRO8xT3GwgbWzhZN0UxcIYU9T1mYYEc2WNBDsplO5UhC5mTz9RvBQlvIOyRD8jChUirjKGJeW9Y3WYeHksv7pg6NqjOX5RFFN45ccmIGIUQi5w==~-1~-1~-1; bm_sv=6FD95F78B34E52701F005731FC1D8CBD~YAAQtbRmUlsSEb6GAQAAEQbWxhNJ1p9t4hor1s3r0pXnXwBnQYc3oeTIX8l/C9g+L6/alL68AnwemHnDJ6XchFA/IzGB7e1R1qRUjG8vjwJolRQ7pLmTnTB4oFUhGhPZsLj8871oyodIykGeh05zzoiHjf+AV3yMK3jRRTM6L1zlvaw544rSMPQb3DKwJrmIpI49iLlRFAc8mAbKwut1sLOttcQMginkTSyL7etbKYjdgfPQ2zRjMmEsdBf8cvDuDgA=~1";
    private readonly static string WizzairRequestVerificationToken = "b8c5e0f4bf3d4adea634adb74cda3cb1";
    private readonly static DateRange ArrivalDateRange = new(new DateTime(2023, 3, 10), new DateTime(2023, 5, 15));
    private readonly static DateRange ReturnDateRange = new(new DateTime(2023, 3, 10), new DateTime(2023, 5, 15));
    private readonly static List<AirportCode> AirportsCodes = new() { AirportCode.LUZ, AirportCode.KRK, AirportCode.WMI, AirportCode.WAW, AirportCode.RZE };
    private readonly static string JsonFileName = "allFlights.json";
    private readonly static bool IsOfflineMode = false;
    private readonly static int MaxTripLenghtInDays = 5;

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
            WizzairFlightsProvider wizzairFlightProvider = new(WizzairCookie, WizzairRequestVerificationToken);
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