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
    private readonly static string WizzairCookie = "ak_bmsc=54D872D7AB945C75A9D3BDA091C15813~000000000000000000000000000000~YAAQTAxAF04CRdqGAQAA+O40BRPKBQFTPjcfaspOZhykWnQLvLAL5Eu9W+m5HFKQDvnLCNWuynmt29HI3px4O7aLOyI53XYcQVoxF4W78T1qBKgV7XF4DmFeVd7KyBUMf8H2/hhPLyR7OGWnZG+zgRVJhdRkWKEpdFtsdZmj8yVp4Q4k1OarNUJQev2yzvrS321YWqWHfG8UsgyhD0gvsGzjCpVpjg7iF0OZ7JWbBzBbmaxKB+LxsdCndaHjzAamGSRwp1ZK9mgtZVqQg8bbk6pPkLhKSTMvBUUX4FIvux9D2dfyW+hbjx2Hr57vW47x+ydvGPl9bPKD6canbtGHTHq9L16YpRqEOkY0YUo6nm2JvtVEfDE6nw63Q3eHKIKXnfUOpv0vl9PMr5GW9nyz1qOGsvJnOrIkihB4N8TB9PB3Q2uo; bm_sz=7BCE1E49CC57B86869FAF41F685D9D3D~YAAQTAxAF1ACRdqGAQAA+O40BRONDl2u0h1wYrL8E4adm6eImsUvhv9h/0dS32Jel0v3AryY3dWPZos854nWGVGEKr8EF1fmHSQXug1FDNwwH8tcj6Q+/xSu50ijxt5aCaFjBVjaWAxpabTMvvAG+rOEVvOOSfTlwE0JU5IBmKD8+tXvVtREhhQ+NdgBSjFk+fPXH1Ddm71xC+JcyfzZFPMhxOcD7o1nH7LKJOlj3bHmG9Do9NAVa9ky76Nulk6Y0RyT+167reuRfcaLo+Fg5qZTiY+U+OdIToTsVF/3BwELf0veqK2mtIbL2jz4VMGgR9tGM7MUKSILEk+CNj9r1+OdSv4lCnqjDhDyXqQ1w7ECzmbWHEtynk4FHMCROUa42v/PkcVqRFZWJXbhgekixx1VCTxkTHSJYVUWhvBVsrPgAsQZ62YBuqQYzQ==~3225141~3223875; ASP.NET_SessionId=uxahw1jgpytx2kp5xhv1gbr2; RequestVerificationToken=334f7474b87f457aba33046bebcdc04e; bm_mi=084FD48E5C6F09086B76383328F00CD8~YAAQpGReaApcnf6GAQAA0LE7BROAk2qhKmkUinxGKTgE3hWoO/4RNv4XJcHLZ0yreXIirT0ZG3NwG/gTFrewCcKKBzURIxlI7qJ8XINb6o6jXMwtZ6f4kF+KHKecxW80Oiwlxcb3bzymCOstT1SjbY2ecrgD7EX0bavIAR4e/f27284YfTChR2JfqHS3VAakba+nXUYutEpukJ/A3hRL89Gy+07ZE9bh6FMic4WkZM8Jl4xQP8IFRxeTZeUVJrLV4ZWVOVOEYZOwbReRt2s0fjGe0Jzfuxy6AS8WAn2aF5HEKemAM/UhU5WVxx/39smd2EqRYI08jFL4vMyuU91Xj3TZN44kEStN5WuKxvmfa/1LyAZgu7ZCmsVARdq5+M4=~1; _abck=1A042ADF9189B402227285B407781B30~-1~YAAQpGReaFxcnf6GAQAAh7U7BQlgWBMQfgWXrvZVivXfaOF2rN5Od6wwMZGh6Ec2nIml613Y86FsNxPnXPImXLcXyWN6YZ9jh83OiT+0ZoD/G6PnHTvAQOhvRQ17n2AhqMqM+6qrZd5aVK5Iqxcv5n+G3xC3YrQgUahkI8a/Ocgr+pjFEmZ41Y26bEbKnEpZoCLUaSACeaqh8TBAK1n78Vp/iXw61O7sb0kZ+KKEHluny9cDcR9I9ia7qkJNzGPe4h+r8HdJ+V6h7WZC/hOV0hwOXm5cZEF/rCsmC5AqUwpr1c/EbngwqRMgKLzWsSw3bdQElwBgAKdzS5dPwQ5im9O+LDV2ms/SGyFmfuSJ/Bi/0K2mfcM/X1NtBSVZeBGiAP6nP9cwRJkUt3yxKaWkZWiYkrTeCPuFJX72O6orFkVdapZy+9IrIqw1UctNm9zE+SU15kjIBe5E8CSr8L6UmAB2bMXi~-1~-1~-1; bm_sv=60DACD6171A6D34863BA7B59A17354AD~YAAQpGReaF1cnf6GAQAAjrU7BRMUxDBdb3PvQaZnKRw9cMNKo0WTV7fbIAwuhAav0OtrMIrXWZX9j4+jkbwzbIic7zP0rvklYdMdN4xDQwf1jbSsfbWwJ0hoGeSYX7yVaw6yfDyIstatkoUyKF1BeXc5AVHyn+7XryMOJoWyRpr12LM8+S6OZro2y4MZYMcjgMLnx+LK/euO0VrKYrOMYPdJedyYdG8ll2wO4AVuMJq2oCJxvpvajC/B4PxkpmBt7yY=~1";
    private readonly static string WizzairRequestVerificationToken = "334f7474b87f457aba33046bebcdc04e";
    private readonly static string WizzairApiVersion = "16.3.0";

    //Search options
    private readonly static int MaxTripLenghtInDays = 5;
    private readonly static DateRange ArrivalDateRange = new(new DateTime(2023, 3, 22), new DateTime(2023, 6, 22));
    private readonly static DateRange ReturnDateRange = new(new DateTime(2023, 3, 22), new DateTime(2023, 6, 22));
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