using System.Text.Json;
using System.Xml;
using FlightScrapper.Core;
using FlightScrapper.Ryanair;
using FlightScrapper.Wizzair;
using FlightScrapper.Wizzair.Api.ResponseModels.Timetable;
using FlightsScrapper.Workbook;
using Flight = FlightScrapper.Core.Flight;

namespace FlightScrapper.App;

public class Program
{
    public static string WizzairCookie =
        "bm_sz=24F6EAE24E1C91B9D3E811C40C7611C4~YAAQn2ReaLp5U7iGAQAAiQL2vBMzBtFZ4YlR3WxKHDLvi7MNVr+4Z/KbV0x0tlFzVUE8dOP56tuSS+YDb66P6qGR8X8d5KQOOkc6bEAgAj9QBrqjpOVi2IisP7DSAEUwoeZ4Eo7JN5z2US1KsxXvvKHlIIIwjIOZK22aiLsDMhfVQQb58xZUh0UfKwOwvM+VxNfi/l+Lkqq1oqReg2MoAdINgWBCc0cDyfqK4FZqAlIxXRni8lm0kXYSih3y4csZCC9qdG+Yi3+8iuaBkqR2OFnmmdlHEj3HesYNvF72n9bIaNwjzCX67IfE1DoiuMQ4b5lAnlHqKEsitUaxs0hYEaG2WfFBIhbXp++vO+GSshduhFcqz83ZytzdMIRNl/ewpnsNkqqBAdvFHwJr6kn8ImOe6ohLPQFjOOTnczgxM0I4nd5svJm1UfrjZg==~3355956~4403768; ak_bmsc=2546662C084E83010120344A4FBE54DC~000000000000000000000000000000~YAAQn2ReaN95U7iGAQAA5gX2vBOHXZBLqsLdpHbwi9Qan+yex2gzr8ksqfWYA/POnoOqWUeWJXpgrDQV8XEAcysKIjH3wF+HdgKKIOBxOCV124w8T2/axldks9eG732p+YOMtlJcbE0VQPVCdmDV2ByRHNzIS9L73xRsKa/Bq6KGRmsr1OakSgMWDrYgUgCzwg87Tc5w22tQ5B4OV8qtsXYrf2Cj5IT4G/974LkPXa2dZlAX3jGdiU5f3fuBxO+VsRHtqXVmpMClNxaGeRFGE0BsQgz1AgL+C5l1uy4gBVQTHkFeWjA/q0hehFLFa+sXQ8uruzHNirtnXWAVOhVqym9eBzsnTq1YqjEhGchEy0cQjN4I4/yeMWly7dkygiX/apUQzdNSe+kGRzTVuu3CoN+2SJNJaxT2oMzGYMZq/lBh9kyJ6brS+sRz0fwZzN9IsWJos6ap7TbwSOr9oWmSZO6M8bbP+8DgZaYiLdxMBda4VD5IHtMD9edXzR2tmwFta/qIb2OEnth6ZuIGCUChBbavR0QW; ASP.NET_SessionId=jrbs1pqpfnxzduthgc1b0cch; RequestVerificationToken=c1104841444c4e6ea30f67b262bcdaad; _fbp=fb.1.1678207486530.274994336; _gcl_au=1.1.480142282.1678207487; _gid=GA1.2.734996339.1678207487; _ga_G2EKSJBE0J=GS1.1.1678207486.1.1.1678208549.0.0.0; _ga=GA1.2.2047669040.1678207487; _abck=64E7D99DE11019482D523742465167C0~-1~YAAQTAxAFz3KiIeGAQAAssQHvQnEL2RXmbWjGnPToTtUzoKZu27Y5wFJTdRLBbBsbatlnTzfZBOuMbm1VPNZajXUEKFqkMi4fhlDqP+vy4xopn/BtyqlaRQPzXCKclZ876725B8tkyYEu50S3Xc/ZaG5bVwgw6Ij4JIV0K67Mvi+AleJApPzoMNU6mheb2zeoBqBf+qAD7KzJnN6SgDSW4kMtwNFUC48CVRZv023F8Aot2FHY8u+WWzNoySy0C15ltTJ3AtL1U+8luTni1hyW1y6loUcu6osmfrNv6CRllp8yv+3EjNQnV6DxMjt19I5JISwffQDnLKgtsIDi8c9QEobi7lKNtMySwzqanAvqhNaeMhc4HLLEnTY1eXCZPLQhdxDJ/IJZ2uPrZYBnAyrybKxrxaE465MNIrQ9MO6OU1ULY6ywrFiuOcKwjMAIoYCXxkW5y78zrVSONf3be7iPyiqEcFl~-1~-1~-1; bm_sv=2549D3402FD4063DC756BB2AC0E81604~YAAQTAxAF0bKiIeGAQAAu8UHvRPTR1IavIDZhw+NV+aYpy/9cOatyzP67/aenfhGtQno6M6vu5tTHJ0KA+a5CGacui68kcP5xOxumwyonNh16UhmDcPbQmpwvuD/WoBlyVC6MSrUKtm/ZX1Mgae6MML/fsQdFie7zTO25HL2nznYqXASqH149Vohnk0luCuhI0NyzhZDEmJgDRVVh+BVMzpzXiuOvDRo75OHNC0Pm4iEL1c2xaSzTIp1Qb3G4LUL6z4=~1";

    public static string WizzairRequestVerificationToken = "c1104841444c4e6ea30f67b262bcdaad";
    public static DateRange StartDate = new(new DateTime(2023, 03, 15, 12, 0, 0), new DateTime(2023, 03, 18, 10, 0, 0));
    public static DateRange EndDate = new(new DateTime(2023, 03, 19, 16, 0, 0), new DateTime(2023, 03, 19, 21, 0, 0));

    public static List<AirportCode> AirportsCodes = new()
        { AirportCode.LUZ, AirportCode.KRK, AirportCode.WMI, AirportCode.WAW };

    public static async Task Main()
    {
        const string jsonFile = "allFlights.json";
        List<Flight> allFlights;
        if (File.Exists(jsonFile))
        {
            var jsonString = await File.ReadAllTextAsync("allFlights.json");
            allFlights = JsonSerializer.Deserialize<List<Flight>>(jsonString).OrderBy(x => x.PriceInPln).ToList();
        }
        else
        {
            var wizzairFlightProvider = new WizzairFlightsProvider(WizzairCookie, WizzairRequestVerificationToken);
            var ryanairFlightProvider = new RyanairFlightsProvider();

            allFlights = new List<Flight>();
            foreach (var airportCode in AirportsCodes)
            {
                Console.WriteLine($"Processing {airportCode}");
                Task<IEnumerable<Flight>> ryanairFlightsForAirportTask =
                    ryanairFlightProvider.GetFlights(airportCode, StartDate, EndDate);
                Task<IEnumerable<Flight>> wizzairFlightsForAirportTask =
                    wizzairFlightProvider.GetFlights(airportCode, StartDate, EndDate);
                await Task.WhenAll(ryanairFlightsForAirportTask, wizzairFlightsForAirportTask);
                allFlights.AddRange(await ryanairFlightsForAirportTask);
                allFlights.AddRange(await wizzairFlightsForAirportTask);
            }

            string jsonString = JsonSerializer.Serialize(allFlights.OrderBy(x => x.PriceInPln));

            await File.WriteAllTextAsync("allFlights.json", jsonString);
        }

        allFlights.ForEach(Console.WriteLine);
        ExcelWriter.Write(allFlights);
    }
}