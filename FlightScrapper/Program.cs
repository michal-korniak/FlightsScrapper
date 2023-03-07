using System.Text.Json;
using System.Xml;
using FlightScrapper.Core.Contract;
using FlightScrapper.Core.Models;
using FlightScrapper.Core.Services;
using FlightScrapper.Ryanair;
using FlightScrapper.Wizzair;
using FlightScrapper.Wizzair.Api.ResponseModels.Timetable;
using FlightsScrapper.Workbook;
using Flight = FlightScrapper.Core.Models.Flight;

namespace FlightScrapper.App;

public class Program
{
    private readonly static string WizzairCookie = "ASP.NET_SessionId=bplo32v54ee3xmw2vogaux3a; bm_mi=436844B7D02EF5B0C31D3C8A0C9AC4DF~YAAQTAxAFwkgloeGAQAAtj/GvRMP7Okm7xeBt7ZcLln1y2gtKEcMq8VL84shFhhyF+BN5ehvuqavfY7TglIHfb0Rv2HCphiTlarBdHC8TH6DFAo3nK2bLgia/twch7uNdWogkYHVcv6H4augM3N46wB4M6SyrVcNCJv/W52kNbwCAH5jd0fhm+EhFS/goTsI0DgBnkVmupwcmQNT8+0QKjJFa7N/tWze4AwrEFtp7Dh6znaDTRPbPqO15VPueCNjObqkKFNQK3vWFuD2VABA24kziDly5JIyBo2sV6jH4TqC+V7qDtoGngDN6g/SYC78FuX9GogiOnPvGNFFt7TLdAo/r9W4a24ZV9HXKhAnWSM92QnMO5qQPfGzhTw=~1; bm_sv=123388721AAAFD452DC19EB6643845C8~YAAQTAxAF1YiloeGAQAASk3GvRP8/eTv74M9KKGDRUYGIgJlAzPt6UaMwRGohjT08mVTljQDA3at8+a6RST2S0DAidEY1f+uSLJMZeEXobM/OmUn0PPwMPJkr8oZFotVXt39Y7zZPzOuionXl4gOIsvEvEewAiqWLua0A32Eq3YEWey4BnfSH1YPCkMtoeT9k7bQ4t6DOxngE35YMaELsiBs+harPoibVICtOMETeLGndp7Z5Ym+/i6f/cY98zv6rQE=~1; ak_bmsc=C094306AAF6DCFA6A6EF3030FDCA70A3~000000000000000000000000000000~YAAQn2ReaOznur2GAQAAqgbZvRO2HZbLa4RFOXB5qFOq6NaiythvaZ2u1FYMFYsqh95AigYgVDR2jzrMQRLAVo6qUFx7vrZ22rRjpHxIXvGoXWOvCrlBALxq9eJuOddcBwtOdihWnMOhSSqp/F/3dIuQ3wJ974VU+U5L92XpYFH+eRQi8HIXjhca1StujahwmQzwZRb+A/3RGzdIYgP3WHCujggZQ8Fyk0b6Wg54zrvWiBxzN3nnq8bxrfW04xmv5GQBxmjz+W/XISS1bgIAxLkYwyCsAOZrLQU4M8Snyb15+kd/+wffGMLdmhH6feWKHOI8VYUemoEy6dPIujlgmAfpLHHbfhebil2tV1riVrm0pmdfdyJD77J3LcZ6CQzZk8NQaAiDMISZ/8ZmHFBjQa6kRo70FWggUENWPE2jJWhcRI//qKrPt1OrBUMVwPxHbVRgPxTKoefpkaPQcZcejG1RMrHppfyz0mxBhf7q6JJENn7YzLrdxLpeE7erF4kSKHZ8VvR6uLm4; bm_sz=5585DA73AB1A97EBCEF96A54404031EF~YAAQn2ReaO3nur2GAQAAqgbZvRPb58DswXaOPXWwVKB4fEROKzN3+EkgBTMjrj1ujevWy9McLxBI/bfpaB6Zi8dXfDL4u3WYoWDg47PmMmgXpPsznFpjsQTMtVb768T42y16M3qMnvE4QTB5gaMIepqI5oQn9q5qUFky2BiNulWECFaFMK5E31y2LEysqaXMOP+7kRnJVnSzw8q7H8gzvncCbKxl8AQY5GWGmTxFjUBQ4+MxXX/11Pvv26tl5tbnFJViaSOjDdCwTGGpuasA5ED5eIE2/RCmcPKEFBzkD4lU+6LZGUwXNYpi3wABiw3L3CPe9HHDjXB6cdcHI5Ncm3DzDqBXRdMML6nG08eInJ5bbvdy3TC9ixKTfOWfUUm2+hJzJMilNhgsQGbCqS3D3jO3X3BrD3HJb/KK9Mnu/v8bNE+157yQXFPfA2nv6BNM6BdFvrnM1xpzQ77XD7cfe4HY~3686726~4272706; RequestVerificationToken=77632ac7b3034e5ca659aca465aa9013; _abck=1A042ADF9189B402227285B407781B30~-1~YAAQZQxAF111v72GAQAArBXrvQmEpLLKuZCPfwUhI80+EitIXeYCHtVJFIoqpHZz58JfrMOv5bi60jq+yteXfgbd4F7eHH+gQqr9y95/c0J5SUqJrQWZsOpol17UD+eNH9Xd1SkUXaCme4KkhdjwNdV8kHOqKmMGa3uZ16FRZtJcRLXiS3BhzP24fKOVPbdQkvpEBunHJn21W1I0HAreJOoqjT9/2ZLRgjzTcsYbbu4d/rMgb8CNM7A6BG7HqV6h4QAkhxyYqR8ZK4Jc8XOmX8Let6p2ahGPj6Ld4s4/1+G2Tk8Q0Crn2LhMn2P/jN3cZEIUvZyaUQ5dptq0PjiqJjzU5zAiAp/e24kpAY/BbWcJaUVfnzOK/ElZHcTHCRMubzVN9tVPbm4dL+RGwsQY2yUPHYKfa0Uwg9FFegcXvEbJZyOcau0ep6npWvzg+V5dMZKlPrvx~-1~-1~-1";
    private readonly static string WizzairRequestVerificationToken = "77632ac7b3034e5ca659aca465aa9013";
    private readonly static DateRange ArrivalDateRange = new(new DateTime(2023, 3, 15), new DateTime(2023, 3, 19));
    private readonly static DateRange ReturnDateRange = new(new DateTime(2023, 3, 20), new DateTime(2023, 3, 21));
    private readonly static List<AirportCode> AirportsCodes = new() { AirportCode.LUZ, AirportCode.KRK, AirportCode.WMI, AirportCode.WAW, AirportCode.RZE };
    private readonly static string JsonFileName = "allFlights.json";
    private readonly static bool IsOfflineMode = true;

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
            await Task.WhenAll(ryanairFlightsTask, wizzairFlightsTask);
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