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
    private readonly static string WizzairCookie = "bm_sz=3A4517943EB1A941966BC49D70DD5119~YAAQzrRmUkgLaKGGAQAAPVlAwBPBPZBDP5qxzRVgHj5WDLWregye4HVsaQdxG+H+5GhfkMmsLJbXzqll41fsWV6mH9f0OFMk2hXKlK3u2hEZvCYY9g77++sltfRpS6YVeB2z191b5UX6kOUAOe3ZiMB8G+D5RXzjIImqwCPDKq4Jfpg0yDabzhaBJFS/s0PFhk9Aoqjs3e1cvIwqTh1fdRbOdJQ0lAn3bz6gvrdq2PM0kPe6jsGoHEkiC23S3MZ2cfDZIvLBKmB8T4+4l0RKY603/vNOjnlJwfobZAm94YSzfnn4gdi0ELK5r4B2mHxF70ktJ96QFJuMnZVLW6THbwJH/F5VrXyrUMM2+DtuY1xnNnuJjHpMtnekf8RAqwtjiq3PuBBr9euAzepDV/QWhB31cng=~4277570~4403780; ASP.NET_SessionId=vvzwb4jobdc1a33v2ymcsrh2; bm_mi=4DD163FD668F03413957C6D48376EBAC~YAAQzrRmUtdCaKGGAQAAdmJNwBNlJSkb9RBbU52g2z/fGdgeCyTxa18pKq7u+aRJRlz04wnQLZNdYW+oBkTWgn7bUSWYRWU+cJ+5QpaN+uJl6Vt1SzkhKrk9GHeXP3HM1LT0MBCaujrI360zhDM2Sm4G3bfUZZtWRRRgAvfjL0VTduZP2GCAd9g3UaesDCh9IyuzfRYkg+Loq+lL63F+e9TYLozUCkrmt2amTFhCPCuBbSnCxaJflloC0E6N1lTH27aIugNFMm48M/rTkJhuD+DrU91QcbEl7DKmedW5LaH23SKcxS2roARs+SdML61yW2/BcpLam2q0nlyMXWYXwVZeWKcUwiW8/iGgtSbd68DMlS4filhi91XAtDdNdQ==~1; bm_sv=6F5F0289A138C97232F5EF0C73C0B4D8~YAAQzrRmUjpDaKGGAQAATW1NwBMka24adj0tLSE+1poF2bZEMRfJH/1mv6L2DV+i5rYeeHldPNPbV8/KVuIVzCXTqq3KPjCagOeqlReCHjSFjysTtjlqWjw2QyZXmh374UXLoqC5hAwiCyND6HzBB4WLQbdZ24SHGRvTs+PxpZ5zhr3XV0E15US0ehQoYWQVPYdEuU7yMk1SFEhRIaNFRTl654sJhTSHT0LJd+LOx+rbKdJgz5iNW3dxGvdIpzwkTAU=~1; ak_bmsc=AABC3EF07106782DDC8CC2DE7184084D~000000000000000000000000000000~YAAQHWReaJP3V4eGAQAAxqxPwBPbKGU0kbDlLDuGHvs62SdyO1B4poLtkvY4KKAACbjtTES2u4ghsgLmJIYXVv7osZ2Hmh/vQT5dU2t8sen/MvrpWrdHVEvdiFRHWEnfJX8fIKXl6+9c4vC2nVhaBt/ATU+r8aP2cZyPfcUCasMqfBwfn3zEhMw1B3Rz9I8tCPbMgcxvEBHl7umx7scFuHZ4oGEeVz60njf5t2mZ9rCHLwoK8nk+PlojTKzuGKM/KihgDmVROh9rgIPQh+iPTtfVou2Eycwi2xDY8R7J51Al5TGR6pcalEI9q8D8/p+xy0T6igk5IRnnY6eLln0NQ67D6bSb7rvH3MSSgJbXBix1Smbd6LbeXIB+ULQ7QUKWL8ZagJ1t453rJH42MtSvOo95lLROhE7x93ChEZtlLUR8L0LqlzI7oGeADSRAFLQOQAePjUevzbG1SmMg8ku0hs6N2T3n4kEq3kNwBUq+/4Qt; RequestVerificationToken=71530a635b524560b0320cdfa94a1ff0; _abck=944042F0FAD3E97178C799F8A4FB7ABE~-1~YAAQHWReaIKzWIeGAQAAP753wAnyJs9v/PRP5a90tk1eu0Zi0XdQ6pmN6B+k8k2zuHmC2RC2q4sV6sK6Uw3ZErhYSOzivUDtNVORf1WhovLcAfJLsihS1Q+wLTqrgjf0Yu4FFQjb4WEbDuHIO8OnBvU8pHnJJbo2Xx7ALN1/1QJPn1PLY07tU4OfSPoc7iuShs7OlIRwhR8pqCr6l+j1Ho8txQV67wqXnbMGkUuwuvhaN3YQDznRQZq+QcgFcckvfm2t9jFGQbXhTG342ETY03jbUYPDKtAQ45eMhNIx9Vd3cCzBiElLDXuM54o2tDD8Jdd/OkF/EOxKCNNjyR/ZLXUfM5wi+Tpl+FDIo8OYeZ3/7WzS4HjvbBNQB4Ru3XUKiDQCbz9iNOeRbTlwnfiCe0iSJiFlSBoSzbabjGIszpan4AWid5/I4Ht6UWHNtDBKbt7gF5kzqtYduDa3sw==~-1~-1~-1";
    private readonly static string WizzairRequestVerificationToken = "71530a635b524560b0320cdfa94a1ff0";
    private readonly static DateRange ArrivalDateRange = new(new DateTime(2023, 3, 9), new DateTime(2023, 4, 01));
    private readonly static DateRange ReturnDateRange = new(new DateTime(2023, 3, 9), new DateTime(2023, 4, 01));
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