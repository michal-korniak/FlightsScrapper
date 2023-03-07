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
    private readonly static string WizzairCookie = "ASP.NET_SessionId=bplo32v54ee3xmw2vogaux3a; bm_sz=5585DA73AB1A97EBCEF96A54404031EF~YAAQn2ReaO3nur2GAQAAqgbZvRPb58DswXaOPXWwVKB4fEROKzN3+EkgBTMjrj1ujevWy9McLxBI/bfpaB6Zi8dXfDL4u3WYoWDg47PmMmgXpPsznFpjsQTMtVb768T42y16M3qMnvE4QTB5gaMIepqI5oQn9q5qUFky2BiNulWECFaFMK5E31y2LEysqaXMOP+7kRnJVnSzw8q7H8gzvncCbKxl8AQY5GWGmTxFjUBQ4+MxXX/11Pvv26tl5tbnFJViaSOjDdCwTGGpuasA5ED5eIE2/RCmcPKEFBzkD4lU+6LZGUwXNYpi3wABiw3L3CPe9HHDjXB6cdcHI5Ncm3DzDqBXRdMML6nG08eInJ5bbvdy3TC9ixKTfOWfUUm2+hJzJMilNhgsQGbCqS3D3jO3X3BrD3HJb/KK9Mnu/v8bNE+157yQXFPfA2nv6BNM6BdFvrnM1xpzQ77XD7cfe4HY~3686726~4272706; ak_bmsc=DD3C9DA205FE3DC4334F87EDDCCFAF74~000000000000000000000000000000~YAAQn2ReaLib/b2GAQAAOog0vhNDIJtjcBK/2eUFoVYp+0A8Jrzf2T9XV+xCsLMsKDKb9lvhH4BOfmulQwf7DP65y0dC9JW3JoTc8Kt3sX5Q95pYUqpRyI+Rt3AXxKAc9bKUqDFQJx2EoTfdUFwK2v9Rcnnd4rRPTODCJl2KJfCxBQtucaXzAidCoguJOQFIRf8Fw4VaneB/xWDJCjZObkKI+DNaDSUTZ7ihtmt8YjJcDyASNVhv1xQgfXL2KpNYl2sBQdEJOwvOzG8p3u3vw8ZcTYCdhzf64wz2nB6bGkrXewlW3/aepBM3Gx2vByE9TJj+0McwAjWEJyWrD8OmS40oG8eRnKsJbqHPuTh6mtxnR5vEzM2UcPfeetD9V+i1jNFmwcly6UOIyT5Dz6P+58q0vvzYCUz1B94xCbHE1uhOeBTp; RequestVerificationToken=4d0bc6fcbf99455f9e45ddbdec8513e1; bm_mi=6E55DA8558030E4D5141E55CBFFD0E48~YAAQn2ReaJwk/72GAQAABz5EvhPd+sTGrapOizHadXRIVFGiGczB4a8OyJonlCX88WaxavGgAk1RWG+M+q+VCfRCzHBzbjbHRJb2XmTe/YwzdaL6V5QDPW+ZSXoCjywnkBR4roE3nFgsfsVj+lGe+guEXIl9K2ea65/ObVffU2WNjTR+DPc9e8r2laHSttU2omrf+j6UliSQvEtxuFQlMEtHW6CYfmhp00g5IcWIld2Mq8ROqDLEFgNJ0ZGARkfwgePrvOFAVVyxxSc7hvKgv8R3PMbf8O2mFy435buyRq0HwriNLucYT8pYJb4=~1; bm_sv=E4B6FBC1AB0655FE41898F49CD642810~YAAQn2ReaFsm/72GAQAA8FdEvhNbjwg3KsKCkekbAZ7a/cO4Tzi9stcrhEN1Y9Xs/f19F0buGxO7EPJ6QrCjRPP3zcNhM9q19GuhgfLv/zqsCPogXdvR4032sKXuZF/YGPwboNqxNdSTVMrE+iMcXTDwWNEbZcSk5ePXJFv9AF5xRGwSUJVpSW1kIvfnXLD7Vb0o7kimC6Bc9mOaiT2t+3RMsp6Mk1MwpbNxUoa1eX+0wsaju6XY0Tm90dob+1UwvGI=~1; _abck=1A042ADF9189B402227285B407781B30~-1~YAAQn2ReaLQm/72GAQAAdGBEvgny+JGV6Kl9qJHk1TokAuFyLWZLxaHihbS2EnC7gw2CBZNETkd+4eTldbHyaoJM/WOuK0uPtYjYTrAONVZNKz9oJLw9xHvdSA00MtbKtPST1eJ4dv5SAvZHTMlrCiXpinUGMePFGwM5TV2N85wb3tQsxddA6ujN5oZRbObsIxZptlDPzpJW0CFjDQtaaefqrBAYCNZRf/pES5NIvuxSWyNLgSmzYpYFWR5wYnRhGIea6VMty8AzcUIUrR3AKCTzFx7qAOO86iY1EHQy4/jo/IQHJZXaE+pwzW4Hdz4ulVAhthaDecwy0TfX6PJ2NY0UTDYFl+ebmU/eA847/3B4at2yXYkAMAmbF52crWRJLLpiO6oaCjN0V8F+YVkr+0ZzcJWQIVFZFR7htlyFavDelixCv1OUEm63W0Y6DMuYup6KMJU2~-1~-1~-1";
    private readonly static string WizzairRequestVerificationToken = "4d0bc6fcbf99455f9e45ddbdec8513e1";
    private readonly static DateRange ArrivalDateRange = new(new DateTime(2023, 3, 08), new DateTime(2023, 4, 01));
    private readonly static DateRange ReturnDateRange = new(new DateTime(2023, 3, 08), new DateTime(2023, 4, 01));
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