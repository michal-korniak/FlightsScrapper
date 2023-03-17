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
    //Should be changed before execution. Could be taken from any resuest that is send by wizzair.com to its API
    private readonly static string WizzairCookie = "ak_bmsc=D362C62F57D1E3E7F41DA5FCF6867AB6~000000000000000000000000000000~YAAQtbRmUqyyhNuGAQAA3RMy7xPzHrHwAiq0PVwPVawqgZYtWe2jJjK2NLZb006mGA65F84vUqUeUsfFY83Pa7vw3jwNIk8a2gvfNvsBX39ydYaf6KHMLYX9STN+pFixSWY69I2epu7cmmg9lAbruAqdxeBo1Lsr6ssCoB1hdmbEQgCImlxexOMwChyS6gYs8byyMrToe6nM6CSCsJrZln+9JCsiQ2JAU5NDpuAvvEptLIl3CuIXOZNAaMVhssCjfbC1F+Ouzd0yS0cD4vbC2mih/AJPhRDfBvNBZlWK+lWSIvm+rCHElrXrocNc5Og/35sl3lwFnkaNOVlkvV1TskTLch84upU0mr8uHh8IHfs+4QBwYK9Q54SYmc9bERgFZwKI1Mk6ogIbtd89zQ==; bm_sz=2A65F8CA4CEC69EAB596B176B985B069~YAAQtbRmUq6yhNuGAQAA3RMy7xPUDLDUUhJ+T9iLI6JGBpDZlVrKl5bjIKlGAT9Sc9n2raVDz7RWE/4cJD4QAy3QZNV/8qaMNDkScgTDAWNnilLkPYTJKY9Sla56Nb6/XYBgEgEet+7oU6Sjj1JwplsvtLqftuKdUMuuUSaFxDdyrZ38AA84n3mbb9jgqqnx8HsLhwbpcXsPaLM7lt26zppc6zolZ5iDGuAwN4ZuVMFa/hz/gHtTkLtjYppbz1VwVlvU4L854YvxdFDrbqm2U77O45hu6irrHWLdv+bwv2HHjx83xQozap6fbBHSMRmLydFNmuQs7EaVDh1H5+1k62zfdfDXU3uhDWTnd2Kpgy0tEvnunxJIHJJXBF+0SiMi+4EIoLbME49eWoxw86KUeRHQzg==~3160377~4403778; ASP.NET_SessionId=tajc212upxigaljrmpo5m0pd; RequestVerificationToken=cb4d490c19b14ffb9c83ab660b3a4a21; bm_mi=1393304119E2D3B608FC9EB0781D86A2~YAAQtbRmUhOzhNuGAQAA0C4y7xNfNTgknBaKTpOf/YQNO3XtW72hpz4lK0jovmCuvwltcAib9lwbrohIGJ9dUyPyDwIR3oP1mYUasv3iK+wov4JbcpUtYmyqjCjf4rhJFkkO8oozJGCf5M80q4Wkaf0dl1h9P+TfnPiG5Du0C+8h+7FWca2HT0I4ZryV2lAWrG59SChbWNVNG72xUPt1BD3CvR4/sCMkdeNVIuwj+aNHv/lu3LZyf/rfdCDh8CZxfqsetShFkeIRdiUoGKz5DHzbl+zBZBobon3Rcbon5AE/8eYb1fZ/OxrWcWVPA99VAOPegG7ecZU7wHIvnrVuFJROPII=~1; _abck=944042F0FAD3E97178C799F8A4FB7ABE~-1~YAAQtbRmUruzhNuGAQAAXU0y7wnuBi+9tUmt/UQayEhDCiK3d9D8HISLY9XOtSBf/jA50n/HYiZ21g4dpEWa2o2Hiw6BSZfoeOFwY750DrklyOkBqu3ejuxwJU/zGrc2LawCEezu2ImNZH4jjYmpz1wT8rUx04R52TokWHJrJiFD58OLo39gemEXBvOehjFB9VdSYeYT+03Lm1zc6AkSVX4ofccyzjEGPI94qHRv+/DcpZVYp6tov7iPyCX8scBWJ0Pw6/lGg2ucX++Zu9Hb0vfsQd/NextLNHZOE1unLnf5nIm0Cbr7g9Aiv24Qj0JMDqPg6TVtkzIl7WxKaPiZhggxg8BNCPnxk46ZmAsi7BDoo52ImCrqaZUxiOtt6NWZSZzBo6Coy84iekt+938kZTGVX3gSSCkX4Q/yyerYiWh8k2jmN9xhhHh8l4Tz5+cODDwHrmrbstpCwipZmg==~-1~-1~-1; bm_sv=897C9D8840DB187F94A563CFB7EEC918~YAAQtbRmUr+zhNuGAQAAFE8y7xOVLiLdkuw3cvnFKtqd2iq3Jm9hsF52PQNO9HKnvfWF1aBsjzjIAiSKsa3sYrHv9f001qKfvbUAjjnBXde55Yv9u2i91AVXI7416p1pIyuxtwHUKMNKmXxNXgh+P5jysEyisKPqg4AZj+ANTLXnrknASWnbl9w/KsnLQb9ndriXX1Se1pgV1tHdgXIBeNLq9bKLwX2ysUbsb7+yC75YDAWO9B2buV1kJyzhJBdcsC0=~1";
    private readonly static string WizzairRequestVerificationToken = "cb4d490c19b14ffb9c83ab660b3a4a21";
    private readonly static string WizzairApiVersion = "16.3.0";

    //Search options
    private readonly static int MaxTripLenghtInDays = 5;
    private readonly static DateRange ArrivalDateRange = new(new DateTime(2023, 3, 18), new DateTime(2023, 6, 18));
    private readonly static DateRange ReturnDateRange = new(new DateTime(2023, 3, 18), new DateTime(2023, 6, 18));
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