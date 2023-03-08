using FlightScrapper.Core.Contract;
using FlightScrapper.Core.Models;
using FlightScrapper.Core.Services;
using FlightScrapper.Ryanair;
using FlightScrapper.Wizzair;
using FlightsScrapper.Workbook;
using System.Text.Json;
using Flight = FlightScrapper.Core.Models.Flight;

namespace FlightScrapper.App;

public class Program
{
    private readonly static string WizzairCookie = "ak_bmsc=CF964C71DECAEE513C5AA96EA5198074~000000000000000000000000000000~YAAQn2ReaLFGAsGGAQAA1YJbwhOqPpzOp0gvTxI3NbvzCHuIlyjMYcwpTbJSQlA42nbqB/K1qpQWdLLP2BrYLFzP4yG2YNluIy+D4keQLPW7Re7tcKFTMGFJOQPv2zvOAA+k2/fiVo7op5Y5Ag6EV15aMBcucxFWc3X7JexyIyxDIYk/VAM6swtwr+Bzcl9HjQuGuoMBqKzDFdq/pgag/vCRI1vHmFr1n/O7WK7o6CwH1v9w8a2ibMa7rX8gZJ8Aei0Ip7NKo7YGQeSq7tsJJGkx7WH7k12uvp2Qy0MFxWkcVd+6v2FsvXoyn++1TxhNvFJ4fF1sPxHXTtMHv1di0CLaHX5Ilu2/lC27KkAx+I6p0pXqLdf8vLEv8dyqx5vAuYy5OUptP6GUzL89sX50QtGBZgx0KEJLJJYpZiABK9h4vmvq; bm_sz=718347B27FB00D8626F5E404EAB12BCE~YAAQn2ReaLNGAsGGAQAA1YJbwhNw3NvTmkR0mvjePHJcqre3x7BXqheJFn8lISVpzL7cv6j40Sj8AwUHgoBRZf0ZJX98GnUIwjYLAWppTciM9Dd8Rh0Dozv17N78LbTDgltKH5EGoPZL5hyzwGJb8dnq8ao/9aMzGBYTn4YBgT+K3wDHrfpAVAaGOByLJCIsbAL6pWUp+U+fQTETg6Ij41d/zgZVr1cxRR8ayCUzXNBeyNqwuqySgXrj01HcvJ5u05uI8moyW6dupHGX1NtO531bhU4U/Q7DbYn8On4hbaRdY8DBaJxzwf4I3o6g9RjFMQyhZa3AAwaNvE47Hfj1wh0z5hlwVs+5yn/aU/vx9yxVZvs3R6pLMOw/1h2Uj0mS3Y1wriYjoVZu3Puyl60umMpwL/kfMcNqQXFkEfvsejPsOlIMngNoIn17~4273986~3486264; bm_mi=BD50E9CC3B27B8119809E2CD74ED2298~YAAQn2ReaM5GAsGGAQAA2IRbwhMZ5iJ3+Ogp3pUiE9zreLYzjWTzgzMSh6QXObaWhMWUQCpBnhyWusll8dnMei7swfzo4lQpg1E6RmflIjfA1brh0ho9EIuQDu+FA/YKM6lxc+VJWncHd8iU2PPbBusyjfviIuzw2he7DwgtfjXsFHCSbKzJuPn9D5zbwrlwvG55jAp8HSM6ELWyYuwJH5xVLXXnO5Iu5mJarJn30yUWYcjCOr056976wpq9IsZ+ROHTRiHvgieNP1EQinUubNXx9m+s3cWnXCZTNN+cvFnoTF93GsHYXpdWzsSLMJCkKw==~1; ASP.NET_SessionId=w2wuhs5atuvq34zjtocu00xy; RequestVerificationToken=c011e045a169413ebf85b3eea4cb8031; _abck=1A042ADF9189B402227285B407781B30~-1~YAAQn2ReaGBKAsGGAQAAdr1bwglKnS8UpSvWoGXHIwJ2Exho0G2BFAVnn5m26S/gDQyq7V8/wWRV1vi6oi6cFiqVFjlgyv+qZ2vIVGAgbkm+lQuPuFAbcEP6TkUux9wHWtALDiKSyVk1Rujg8Ik0W7jy5revTpMX1HVglKQGDm6/Y4XJ5k2A/Gz2b4HuHuKDqkJIgytx4oWakna7dDQwVuXoZaUChMWTcgQn7SBT1Hre0YXxSkm37TswPd6WYNU9MFEc0ZYSO1lUJ74gOO3DN2lTSaYdMlEZbNEG+MnqDXYufuvAe3Px8d122AXNhnzdtRXRN8bXRPnkn7Y/bQjac1a4HhYsUo3syuTbghrJYrA8WGijBtZWTRoSpx1D1534DhnsjFeEnyvq7XQZ8yk6pQn8vBXSSXhuX/BL8BtxGOmFWtDQiS0BmMvc0xfVWQvR5GH73pF/PSgF/GaAKiavumSLM+de~-1~-1~-1; bm_sv=BB43C0366C6761E971241480F9DFCAE2~YAAQn2ReaJpKAsGGAQAAD8BbwhMsbCAEjjCcNVoOmCRj6Dt17ZaEDbbE0qqz1e2pjjFwzXyLcgEp2Ng/s9EVvNyFG36g45XdzSvmdScuxppyPVBdrzM8bR3vi8ggqFGeJ/HOQ8MZoofIhny5Etn/227OEQa2W88y6cYoG4Mc5hSUBqocc9euDHyVNDMW2Oxh4TCpjwTNHYNeKfp/O4gKn9lPdyqaP3EW2lh/Sg/86NK8DeKCcUiLglASdxYT15/YN2g=~1";
    private readonly static string WizzairRequestVerificationToken = "c011e045a169413ebf85b3eea4cb8031";
    private readonly static DateRange ArrivalDateRange = new(new DateTime(2023, 3, 10), new DateTime(2023, 5, 1));
    private readonly static DateRange ReturnDateRange = new(new DateTime(2023, 3, 10), new DateTime(2023, 5, 1));
    private readonly static List<AirportCode> AirportsCodes = new() { AirportCode.LUZ, AirportCode.KRK, AirportCode.WMI, AirportCode.WAW, AirportCode.RZE };
    private readonly static string JsonFileName = "allFlights.json";
    private readonly static bool IsOfflineMode = false;

    public static async Task Main()
    {
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

            using CancellationTokenSource cts =new();

            await TaskUtils.ExecuteTasksUntilFirstException(ryanairFlightsTask, wizzairFlightsTask);
            IEnumerable<Flight> ryanairFlights = await ryanairFlightsTask;
            IEnumerable<Flight> wizzairFlights = await wizzairFlightsTask;
            allFlights = ryanairFlights.Union(wizzairFlights);

            string jsonString = JsonSerializer.Serialize(allFlights.OrderBy(x => x.PriceInPln));
            await File.WriteAllTextAsync(JsonFileName, jsonString);
        }

        List<Trip> trips = TripsFactory.CreateTrips(AirportsCodes, allFlights).ToList();
        ExcelWriter.Write(allFlights, trips);
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