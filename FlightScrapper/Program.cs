using FlightScrapper.Core;
using FlightScrapper.Ryanair;
using FlightScrapper.Wizzair;

namespace FlightScrapper.App;

public class Program
{
    public static string WizzairCookie = "bm_sz=AAC00B00C00046E0A0034E48BE04A3C1~YAAQZQxAFzriWa2GAQAAMq7MuBOaxFbW48uw2PcykrUikDx6N7LOHxJrz+Fifae+vfO/qRuzz2j9u6JFi+d341IVTkjBhZXsB4FN3w6xYpjFS8zECaYk6Ef50ky3CaqU6hoJsKOPkM4FhOjrtdDg7GP2Ux384yAIPNIrQX1wyA8Oz7nbOJJqNRkW3LIh64O5HUGFGBgss6Uj669RB3uMkXXTPHnfTNUmsaK8QvSFipgUKBAKMnoiEYpT5IuCW3P1hvlIjtYN8gg9xsnYLsihaX91/mQZAyDY35yC1B8HmgpmCr1bowk2oAR5Qk1MVscdpkXN56LihCYa//odRjm11Ct/gJDTgGf8SXBpediFexmapTibWUqOVD2dxGLl4FeDq4N2Fv5CM4cEK4SOhlxN2Nog2AKnSwVY0XdQmEB6Oec3mKxhfTJMZ6fJ~3359046~3555636; ASP.NET_SessionId=vsjoucphjrdd0ig0lbuklgzp; RequestVerificationToken=9ace4c35881246e1bc43977c92199a42; ak_bmsc=C2E34384DEABB57BE9B2FA01B0ED8F36~000000000000000000000000000000~YAAQn2ReaBevMbiGAQAA7Hw9uRPo8wiPOCFI8Wl3FP888GngA2IPunXDElBGOqDsRkK15X24jgaE2Yph+hzpk+piC7I8Niuev1v8ZJv46R/TXz/CffofXd/RLeN26EdMynRcRZimuKFu7+AbTSw9AfLP/UEhnNtXomfKYFmvS4rgD5Htuh1YcMmrCRLISbQZOIAU6B+ArM6Si5r+m5xqFje/LQ2IDo9MlpN61feH7gNGjkn/h2G6IsBi/3UNJjlveEij1FSVCb8XQDain+P2YRNqBG8r44Wt/1LdINUGBoh860QRYIRIT6u8Hi5Zl9yvtY5WWH0FAp8cjPghWX5kBkzfquxZ3InH7r8eQgd4JnqWxAiig/rlLomfams+Z30FQb++cU2zxGD21CnfcS1tdInMg8AWbayoo5L1/Tf6w0ue9vE2; bm_mi=0ED599A08B235B153C11FC29CA9DB755~YAAQn2ReaIS5MbiGAQAANvs9uRM19knQnqvEBy3PgwnddYqmUmpfffEaEfnR62E4+7tY+kKGPDUz35W81v4zZ8zZufoTMtd9caq8+U7drDUoq3rmpA+bjp5udZMsVDd5bAFHj2s66VYTqO4rd0crCc3Tz9u4D/DrtJwUDxMYRpZxQTZOljw3PIHElfpuzUdijmCNRvAg7TYTRTD7EHjPYqyoio76k2yk9Vl+GMS+lpGGmiERLQo/A5LoQm8Vl5mtMQVn35A4zQdqZ2mvRuL9NGFMLz0rZBkcDb8ZJ8m1yX0laJQPXhZe84+xe7o=~1; _abck=1A042ADF9189B402227285B407781B30~-1~YAAQn2ReaB7jMbiGAQAANeo/uQmZSUHQFDeKCaSQiRpERX9ONIOKUshH1Tir5lyUmlsH08rMKCho9YybpA3jOJcqw9CKw+D5B+CJYGJ+rlVg9L9SU+5QJyka/HE/XNEwDK6iT+MswD13Thw0UbNbbSr61sWo1ym0yDlQNMw1Uag6PKqBoph4RmzqmyUUJrEa+0oEreS+PH9qhr5+Z9+SeNGfhwpVhvaN1YEAMpBoZ4OK09FBSuhBzqpKvFhCumulhORJu+KGggjkzlK9nI7Jthz099O/1G6kDTy3Tm16GzqiOhUHs47koytwi9JLAcZ8aLJ2/KTN8hwyO9Y7iBRS7r4irVJW+YOyPkiaED2B1L3rSGk6WWI46yp/NJSWaTmDzRGwB0nvKtcHzrIh1cYU7B6qghBWgNF/gQyjQbwDhBncD3R1gawwFGpPlKQtrGsLCADbskf/H8SUi6oU0Ed+Rqu3Nkul~-1~-1~-1; bm_sv=F50830E23B75F4DC774BDE242914EEA3~YAAQn2ReaDnjMbiGAQAA1+s/uRNneZ+mpdOQPXFO0klSVhyEBHLBUco33WXHlkacQsnHYfaDkiK3QgVtzRnJApmkkIg0ZDr/88Fp91QTP3hn9UjhDOoLoVb2Cn9cAwukNSkwxOq7byuj9HpAqqfC0Sh+3JzSadkZGSZsput4giUBvXl1f3M7jbzXsQ7wuZtikTj+W6A7yaxZmx6HZxf8qMaoTTN1/sQsO52+aPdUbVZsKGgfcRAAV5IpVORcgYT2w6g=~1";
    public static DateRange StartDate = new(new DateTime(2023, 03, 15, 12, 0, 0), new DateTime(2023, 03, 18, 10, 0, 0));
    public static DateRange EndDate = new(new DateTime(2023, 03, 19, 16, 0, 0), new DateTime(2023, 03, 19, 21, 0, 0));
    public static List<AirportCode> AirportsCodes = new() { AirportCode.LUZ, AirportCode.KRK, AirportCode.WMI, AirportCode.WAW };

    public static async Task Main()
    {
        var wizzairFlightProvider = new WizzairFlightsProvider(WizzairCookie);
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