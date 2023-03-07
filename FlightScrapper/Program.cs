using FlightScrapper.Core;
using FlightScrapper.Ryanair;
using FlightScrapper.Wizzair;

namespace FlightScrapper.App;

public class Program
{
    public static string WizzairCookie = "bm_sz=6E83A59B635653111C2FBBEF95855E27~YAAQn2ReaIcoVLiGAQAATAIMvRMpv0oYaxhXOuwbjYU4W0xn8ShSXEU8DFWY1megi9NxJKQUbcTLmgJdD/BycMknhoM6W+eOHXGdNoLyh3gSlue535/9UkEEDze24W2HKFbBZBrOs/CwZRKSFQ7UCHpT7/h7EYtADo+w3Bad1r0AGN01cGA8bw1dvNbEJh+Jlsrdl7wiKQ3TYnbNzHQKR901XyZAGeP0lv1J34XrWNYoXAdExWglhC0ulDCPerDANiwK/qDcQ36ZooN9Y0cN/Xtf8/Vs3e8dYworXzFO/0Y1mtgixSIY/qYB4BN82JnDrs1q29bgmDw7la6r2Q2AFkDOBa8NJ+xsuGtUoj//o2jrDHxZYMk9wKxGZ5Zb2pjZ7BEKuN+Z1A4XKqmq1ysIhBeaJsFxnwTfhkGoJf11X+wqrZxKRxIKJKH1~4408630~3487029; ak_bmsc=C094306AAF6DCFA6A6EF3030FDCA70A3~000000000000000000000000000000~YAAQRDYQYM+kmoqGAQAAOhqKvRMcDryWOjqWSNJyFPrbl+ZUpLEToq7C7H2Bz8YiOfN5IlkZmpOw5BH+65kflR3jgAFE9cQ65EStVjow75V3blXJ0vSjL2bkmyJR0NodQU1mR139LhDMO/G6JCT5ItRJq8Y0wMQ6UCPbRRdgUjRhJCA/yVEVfmaeW79MikJwDLYXqBZynws2EQA3RC0IxMHCLgtEw2YJYR9lPhck6hdFS0oG2g+WNh8xHp37c3xL63IGr6SAj1YM2Ve/4QZEmvSAyGSqWusTf12Fo8HsRuRSn8yLe9zbgY1PxRyCFHRoIt3PD+wB6wh5OQ16HCLXp/LNYcgkix8kilEH1Q2ebIXIioAVvjrCGrBMv36mQIRSJNYa+sw7J0sm0cheZwTl0PmbrP+utVBc5fgjUI0XflC83H8z; ASP.NET_SessionId=bplo32v54ee3xmw2vogaux3a; RequestVerificationToken=d97d50d2bb5e445bbf41be964a09aab4; bm_mi=436844B7D02EF5B0C31D3C8A0C9AC4DF~YAAQTAxAFwkgloeGAQAAtj/GvRMP7Okm7xeBt7ZcLln1y2gtKEcMq8VL84shFhhyF+BN5ehvuqavfY7TglIHfb0Rv2HCphiTlarBdHC8TH6DFAo3nK2bLgia/twch7uNdWogkYHVcv6H4augM3N46wB4M6SyrVcNCJv/W52kNbwCAH5jd0fhm+EhFS/goTsI0DgBnkVmupwcmQNT8+0QKjJFa7N/tWze4AwrEFtp7Dh6znaDTRPbPqO15VPueCNjObqkKFNQK3vWFuD2VABA24kziDly5JIyBo2sV6jH4TqC+V7qDtoGngDN6g/SYC78FuX9GogiOnPvGNFFt7TLdAo/r9W4a24ZV9HXKhAnWSM92QnMO5qQPfGzhTw=~1; _abck=1A042ADF9189B402227285B407781B30~-1~YAAQTAxAF/ggloeGAQAA8UTGvQkNAFrmgFcvLNE+quIi405jDgg1ysDZ3JAO9MSLhS/8w5zQiGs8bgT4Sl2wevH55Bp6qX81L5ChRQILtu9lT9fjfTbdFXqyf1/7aE0qxM4EbQtZBNnrjUesvdnGgb7sqJPl8uO57ZbziY2h5wBLQdlGQNMNnN91hU2R19uyRvdd24Aw61fkcMrFRbRqyWUUocj7hQaOBJfE1dnTAMpJSPQL7UxStPnT9BobsMww+cV6LZI9vyS+/Y4FQjTCsIQpB3I5m+lmkVdOt1EmKYY/FLsrY175PuuWXHEWtqFC42e+WixF4TLdIJgpQ48sz1Ev6lu5AczFHOx2pzYIR3egkhz/ZcEbZgSlRP+79OmozLpMaw+z5nQF5aKRhfmWKOKQqR9wOyvV8QJOW4Q6vqrYIxuSt+M/AB3TI2P6GWRDF3Mub9JnkWZ7v2g3qaBoP+gEwwCp~-1~-1~-1; bm_sv=123388721AAAFD452DC19EB6643845C8~YAAQTAxAF3ghloeGAQAAVEfGvRN771n6qR2Q+YTF7JDX1OMvpDKU0LRsoUmWG1wHmXjR/62G3HEsr7IJ60sfDmPJmehDcwC/HQFU5lekhbepToe96A2x5JjY1f3JRADT7b41g3IL2Pf98eBx7c2hdb+bk8nJPhWrZI7ftOVtfXlMa8Vb2Tz3sVnnqSH295xUo0BWHhNkjfLPQ/NrPDrG8Zh+ViD9XVA+rJ0XjV2Hox3ZighnybvJqZZJ4KjC9UnTqRs=~1";
    public static string WizzairRequestVerificationToken = "d97d50d2bb5e445bbf41be964a09aab4";
    public static DateRange ArrivalDateRange = new(new DateTime(2023, 3, 8), new DateTime(2023, 4, 1));
    public static DateRange ReturnDateRange = new(new DateTime(2023, 3, 8), new DateTime(2023, 4, 1));
    public static List<AirportCode> AirportsCodes = new() { AirportCode.LUZ, AirportCode.KRK, AirportCode.WMI, AirportCode.WAW, AirportCode.RZE };

    public static async Task Main()
    {
        WizzairFlightsProvider wizzairFlightProvider = new(WizzairCookie, WizzairRequestVerificationToken);
        RyanairFlightsProvider ryanairFlightProvider = new();

        Task<IEnumerable<Flight>> ryanairFlightsTask = GetFlightsForAirports(ryanairFlightProvider, AirportsCodes, ArrivalDateRange, ReturnDateRange);
        Task<IEnumerable<Flight>> wizzairFlightsTask = GetFlightsForAirports(wizzairFlightProvider, AirportsCodes, ArrivalDateRange, ReturnDateRange);
        await Task.WhenAll(ryanairFlightsTask, wizzairFlightsTask);
        IEnumerable<Flight> ryanairFlights = await ryanairFlightsTask;
        IEnumerable<Flight> wizzairFlights = await wizzairFlightsTask;

        IEnumerable<Flight> allFlights = ryanairFlights.Union(wizzairFlights);
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