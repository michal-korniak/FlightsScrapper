using FlightScrapper.Core;
using FlightScrapper.Ryanair;
using FlightScrapper.Wizzair;

namespace FlightScrapper.App;

public class Program
{
    public static string WizzairCookie = "bm_sz=6E83A59B635653111C2FBBEF95855E27~YAAQn2ReaIcoVLiGAQAATAIMvRMpv0oYaxhXOuwbjYU4W0xn8ShSXEU8DFWY1megi9NxJKQUbcTLmgJdD/BycMknhoM6W+eOHXGdNoLyh3gSlue535/9UkEEDze24W2HKFbBZBrOs/CwZRKSFQ7UCHpT7/h7EYtADo+w3Bad1r0AGN01cGA8bw1dvNbEJh+Jlsrdl7wiKQ3TYnbNzHQKR901XyZAGeP0lv1J34XrWNYoXAdExWglhC0ulDCPerDANiwK/qDcQ36ZooN9Y0cN/Xtf8/Vs3e8dYworXzFO/0Y1mtgixSIY/qYB4BN82JnDrs1q29bgmDw7la6r2Q2AFkDOBa8NJ+xsuGtUoj//o2jrDHxZYMk9wKxGZ5Zb2pjZ7BEKuN+Z1A4XKqmq1ysIhBeaJsFxnwTfhkGoJf11X+wqrZxKRxIKJKH1~4408630~3487029; ak_bmsc=C094306AAF6DCFA6A6EF3030FDCA70A3~000000000000000000000000000000~YAAQRDYQYM+kmoqGAQAAOhqKvRMcDryWOjqWSNJyFPrbl+ZUpLEToq7C7H2Bz8YiOfN5IlkZmpOw5BH+65kflR3jgAFE9cQ65EStVjow75V3blXJ0vSjL2bkmyJR0NodQU1mR139LhDMO/G6JCT5ItRJq8Y0wMQ6UCPbRRdgUjRhJCA/yVEVfmaeW79MikJwDLYXqBZynws2EQA3RC0IxMHCLgtEw2YJYR9lPhck6hdFS0oG2g+WNh8xHp37c3xL63IGr6SAj1YM2Ve/4QZEmvSAyGSqWusTf12Fo8HsRuRSn8yLe9zbgY1PxRyCFHRoIt3PD+wB6wh5OQ16HCLXp/LNYcgkix8kilEH1Q2ebIXIioAVvjrCGrBMv36mQIRSJNYa+sw7J0sm0cheZwTl0PmbrP+utVBc5fgjUI0XflC83H8z; bm_mi=436844B7D02EF5B0C31D3C8A0C9AC4DF~YAAQRDYQYBmlmoqGAQAAShyKvROrGTzTthfOpz9j5bnM2KAHiy1YnnmDPbwqex3l8hoMOWdjMkxabJQemZhG8xCGYJjH1qHryc5xmoo6m9V7T1KPfGJqxVkXpa+MIrwwUf9qc1wNYymIAUBwHf6mK/zuYtRqY939fJsBwJyNjc0fSduoLTNKJAHxm7c92RaNpbg3F6DTwyTkQRAuA2guTj2FtFZMsXXCSiaVJnTz56HabJo5hwepYy7ffSN9/Prs7pZISr214iJ8lnvYX8nV0rY/SmZJqCJUCST0PVPhkytLiXLXMZOpt3SrjLCs8hYupg==~1; ASP.NET_SessionId=bplo32v54ee3xmw2vogaux3a; RequestVerificationToken=d97d50d2bb5e445bbf41be964a09aab4; _abck=1A042ADF9189B402227285B407781B30~-1~YAAQRDYQYDmsmoqGAQAAOUiKvQmEG4ol/4P2fCZTCrV2lDK2AC6w70X2ZqZNFxa3tqC6RbZUwp04Bkp9ZMK+Wdpf6TD2b5NgMU3WK8AwdYGyHHa5AIlcZlGHcCV2akgFSEaTeRcUFeuT0vH+589UHifd2fSggw2zKoa3eECu79ByYwKhrByqNrSw7uRC4dlA2W07btJBP4CEOMiHAcoNwtZFs53NlOegDSkNblx7HPSGA5A2mEamNVRlcboKctXCk1x7bDVjES4VGsn9KQ4GtbUocCIXrdSLZCabyJ7VqTwAr3lYa3MkRKoS/Dv741+AWSOes69udldVTqJAGaIAWsZIzoifM6PAe2XMIXLc+L/wxLPx3HskgXPygWrMfZHOqVWJYMMqkSZruJvu3QhEw89XxXAL73s0FOG0k9VpamqGzb0610IAxM/yzNcbpHjASO6WMPaA1cFHhpswbzWHHg8ihEN9~-1~-1~-1; bm_sv=123388721AAAFD452DC19EB6643845C8~YAAQRDYQYEqsmoqGAQAAdkiKvRNLihVSaGd+UVGZFKXffriYxgpDEw/Mf4AAIMMf5HDkWVrctQxYxxESQ2cC6ag6U+JahbPzlrqAQVCJqUqWJZhqnSzqHaHMwTDmkSB/kgjbTNlSn2zTXDETSb9QjbcoozgaXQqUBkEFBXEzsLfcy+HY66SOyBCHJrzZibjVje6BF68ndvJtVD/xYoh9gg8c0gRcXA+xF7igXGoc9MrNIb2hEh/DixZJmkLXCOaXzHI=~1";
    public static string WizzairRequestVerificationToken = "d97d50d2bb5e445bbf41be964a09aab4";
    public static DateRange StartDate = new(new DateTime(2023, 3, 8), new DateTime(2023, 4, 1));
    public static DateRange EndDate = new(new DateTime(2023, 3, 8), new DateTime(2023, 4, 1));
    public static List<AirportCode> AirportsCodes = new() { AirportCode.LUZ, AirportCode.KRK, AirportCode.WMI, AirportCode.WAW, AirportCode.RZE };

    public static async Task Main()
    {
        var wizzairFlightProvider = new WizzairFlightsProvider(WizzairCookie, WizzairRequestVerificationToken);
        var ryanairFlightProvider = new RyanairFlightsProvider();

        var allFlights = new List<Flight>();
        foreach (var airportCode in AirportsCodes)
        {
            Console.WriteLine($"Processing {airportCode}");
            Task<IEnumerable<Flight>> ryanairFlightsForAirportTask = ryanairFlightProvider.GetFlights(airportCode, StartDate, EndDate);
            Task<IEnumerable<Flight>> wizzairFlightsForAirportTask = wizzairFlightProvider.GetFlights(airportCode, StartDate, EndDate);
            await Task.WhenAll(ryanairFlightsForAirportTask, wizzairFlightsForAirportTask);
            allFlights.AddRange(await ryanairFlightsForAirportTask);
            allFlights.AddRange(await wizzairFlightsForAirportTask);
        }
    }
}