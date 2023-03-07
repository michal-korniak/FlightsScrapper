using FlightScrapper.Core;
using FlightScrapper.Ryanair;
using FlightScrapper.Wizzair;

namespace FlightScrapper.App;

public class Program
{
    public static string WizzairCookie = "bm_sz=4FB5FCB6D423762CF36FF15DB649412F~YAAQhmReaI3zAYeGAQAAf6tXuxMyz3CDvuvyort9poaU0jT27CZCBloWrIeOwYzdjx8f3POFees8oe5WKJQ785jMfMREk7KFZIAyK2WLUDAswt4sRdQeC/oWZnQbwEa4RZHXb7evQmIYvRb2t4jZc193FmyVbEXi1nkG06LME4sjROPdPMQHP/yfh1dXQ967VUP2RB1A9LEjkcmowUDdNTJ7H6gJkN61xHVk6E/LwAxDnW5lXF6946BLSONeVymzEobeMlM4M9s+EGrDw45WXBuKA7HuEs7sMHCUEti9mI8Ej9jwoSNoPppx2FQybSar/Wad89Hya/Cc/6aaLzSRuByfj6glWCa0iYlo7QTpFFnYLiSs5sXQHmBeCn3M3XIIpu1aGWpD/OBzmU98zDYctxs=~4337990~3420996; bm_mi=06452D9638FFCC10AB7B8C0187B9DFF2~YAAQzrRmUpkzW6GGAQAAHiOnuxM/H9cf8hrHZnnh0fzV5AryrpwI2tWRbhY7MRDHJXHhDekbGgBuL4tGy63w79QFUQmaweERa7GWfPrxEw4se+P0LN1Ji2mnY2cDmdRUhcnmqNA49u74fNUMC+4EkNDyw8yHp3Xj+ozsekbU/VXTfn47+YHpF57/bbXda76IY4wwBJgzpLg5KTb0FBi66BTGKHFaXs3zwp0FagKtfVvYGXsVeZwEVaaa/4pGdU1yf8n0RRRiJMVxe2CxCc3b9mdUSaYxXfGATQ6LljHu9gRXLUkD3VHsUm9seeHdO3FtNqoF5A==~1; ASP.NET_SessionId=0wxfgovs4gjtbospirys1xrj; RequestVerificationToken=bc77494ab0ca49d1acb496cd782c2457; bm_sv=305ACC1D4A36BD2FC93F9DCE3EA08483~YAAQzrRmUoQ0W6GGAQAA3UWnuxO+Ml0aX8Oad1A084gqW2Og8H9KKjT7guC07jBwHubxWy/tAH5NbF16dpO8HzrMGZUNI7tuE2dcnMVWF8C8Cb5MBH2liIS9JlCWr4DY/IDAl2bzVgyw78rg5gKdYEWVgM3AXXKQn+p8GeQgfBkf4oB7cVBcdNF8dy/22hEoSK5RJkMCXvZ4hz7iiV36MOwkpjfQRUbgtNx9004786lSX0RbAmHVgW6TVFfAYVPWNx8=~1; ak_bmsc=4CC81721011BA358DBE7BCFEA5226E56~000000000000000000000000000000~YAAQzrRmUg01W6GGAQAAoHGnuxMGZexH6TsHAuSk/dY6qhPrUUMRVpmwn3eYSCBRRDZKAZy3u/hd6Jvl/lSpGWG9bY21rUhOgWkzflwbc901E+4jFNdurnDhCs6PP+uOeJ9QEwZEhVZdsMgKUsdXWD4NTw+bXB/6xM5F4/hGkQWFj0lYKWv3owbGd5E+WeAM8cu/XnaxVjukGKA00ZV9ETEWTtGN8eVa4hNr15GHSYSbpL046sGU9cLeZZwI61I2kXBPpXBAjtEGi3ZY5kVZHDsNzZTOSYHenzZs//rFqLt/tnZbUUdWmLM3mWhTzuCqBZKC93FHMuxxXkIw+mHtHAdkhcK784C5I7gRTNNw/AC6iVQhQslzSKkqydmKOdPDd0tUzA/VQLfePSbtfbP4zeE9RKwhg02kfdwjHmICpswnUMYYKIVv; _abck=944042F0FAD3E97178C799F8A4FB7ABE~-1~YAAQzrRmUh81W6GGAQAAanenuwnvzyVfFc/jXRoLKpjULzEAkRy2Ctj6O8oLf7c048yG+82JGKR24TVVhOFhkuuKutOtKtTp6F0hbeiQHUupaZtgiO8oEVWMnvy3VlrjVp+l9eYZOWD7vKNbpMrVUYVhmunQ3ry3oGFOaMtgDzS8gvJGprxJqu6oLVPaSVlEYZMgRD1WyJPXo9RF1AR9SKLdVzCvdKFSeNixYMeBkCvt1H5myaQ74yY/me/gSZdSwhQvbQKrnt72k69FGsv8jVl8n4991rUxi5IHGZ9eMHlU9+S8cgPYpwO58AbLvi/0hOMxm4JnapqiMIiJq67oXHD1p4bWqqnPFn2zU3MFPek/0SNz511R/bzqnx5JjkZfYH+JYwDU3GiDtngDMd4ZN5AXDLfzEDxm/4Sq6cZRxi68ZCAr2RgRnY8id38RWihHeICHIBUDBqXDYlvyjw==~-1~-1~-1";
    public static string WizzairRequestVerificationToken = "bc77494ab0ca49d1acb496cd782c2457";
    public static DateRange StartDate = new(new DateTime(2023, 03, 15, 12, 0, 0), new DateTime(2023, 03, 18, 10, 0, 0));
    public static DateRange EndDate = new(new DateTime(2023, 03, 19, 16, 0, 0), new DateTime(2023, 03, 19, 21, 0, 0));
    public static List<AirportCode> AirportsCodes = new() { AirportCode.LUZ, AirportCode.KRK, AirportCode.WMI, AirportCode.WAW };

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