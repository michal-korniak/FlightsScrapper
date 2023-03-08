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
    private readonly static string WizzairCookie = "bm_sz=3A4517943EB1A941966BC49D70DD5119~YAAQzrRmUkgLaKGGAQAAPVlAwBPBPZBDP5qxzRVgHj5WDLWregye4HVsaQdxG+H+5GhfkMmsLJbXzqll41fsWV6mH9f0OFMk2hXKlK3u2hEZvCYY9g77++sltfRpS6YVeB2z191b5UX6kOUAOe3ZiMB8G+D5RXzjIImqwCPDKq4Jfpg0yDabzhaBJFS/s0PFhk9Aoqjs3e1cvIwqTh1fdRbOdJQ0lAn3bz6gvrdq2PM0kPe6jsGoHEkiC23S3MZ2cfDZIvLBKmB8T4+4l0RKY603/vNOjnlJwfobZAm94YSzfnn4gdi0ELK5r4B2mHxF70ktJ96QFJuMnZVLW6THbwJH/F5VrXyrUMM2+DtuY1xnNnuJjHpMtnekf8RAqwtjiq3PuBBr9euAzepDV/QWhB31cng=~4277570~4403780; ASP.NET_SessionId=orgnowrtyr405gskm403ugcd; ak_bmsc=EB699067D6C93541618BD874906EA841~000000000000000000000000000000~YAAQj2ReaNU3AL6GAQAAjqC0wBOvZJqlKmLZqVRVl7vnAAzaSaROC6ZwragxFa6jENq4Y20HxsrkXP33y2vJF+gdYw5kPi+L1StkvHf8EAqrOkKMbal4+QP+NZ+n98hW8xd9YjyIAxW3um2zzXqvGHoSVXp8W8Ox0FzhPmE7+L13dC+40c9o7vEtJo/vQ3j75k8QP19tl3j4YzPoh5HNsoRs8YAZvVTcYHuyY4YH0v3YbI9CLqb9R+Ef5bSQFHnLqrWzpqfodru2HbAwsPGYCgRWoADNyRHfz7zuqZrmMEA7NzcUEEpXma7CHGx4anbtQrqLy4E/jYE2jKCKueY9Gv1XfnOpGO/BCmwQYUytzYZ6QA36pYzBskzwbOfkCKLaCnfo1dBZx1eQnQ==; bm_mi=810ACFBC252BC99A1F1AC66AE39E7A5A~YAAQjmReaBkoAb6GAQAAHZ7IwBMrxEQ8zxkn2MGUprGGGmIDSpMFBW8Pk9+XqoqUB9gPIqR2gPrYVe82dz89WhLzyURvc4zXsqOa+LwM1bo2aqd0YcOXK/HW/+xY+NjjRKn3C80ET2gL/0s6hEIgQrLP63c4rfCBqVk8nMt8AYpRdmYY9DsAV651QPnYbQdqOzgXX42k6aBfUkfV1JWRoRXW7l7UzI0dT82f0KYlaCqJNUHmXGsPCJdglqM5PK5o1kKHceNsgAlZQJ30H047gELNJOmPpNCjc5Y1GCtTPzGs4adF9SsM1ZMie3vfqlza5q9P2Q==~1; RequestVerificationToken=4c21da4476114f7782203a8e5f530e85; _abck=944042F0FAD3E97178C799F8A4FB7ABE~-1~YAAQjmReaCQpAb6GAQAATLLIwAm9+9D7OcSyZJSuiR9wKVe6s3KowgNVTLMnuadHHE2/EeTluwjrzquFPGYdg8TCNJUFp0zdokZBN4ez6OrpjKqr1Q8qH0YWreoIh+BlVuAxVB2p/AumjFbdkUUT+t35CZltuNOwSFJr6s9nJ+aYH/t3iCvHHm0dRnv3tCGqlBKBpCJczAbmvpA8/HBz7fLLKHwGoDCNjBOmhLzRkoXpVm+lp/DoiWRKKeb5kat+2cQvkhSAYwpPAXiiZYx9tWNAu8oRwNXkvEWQvd7U1i/nSlxvRzvPIFxvgh9UheadvXMfV2RlOAGwra/eym93Im6aexKcsOnFZ/zuoBlZeL+gEX2b2NUcHpjXZyQ+Fc+bkax1BoZ4be8C1VsA+aUY7UcWS36itOqFdeRl2zTpMzyuhJeB8/Ru54GYEJwDPWocxLYO2CsI+NMccifhtA==~-1~-1~-1; bm_sv=B4DDDAD74CBF612E9F85283DC3D76895~YAAQjmReaC0pAb6GAQAA0bLIwBP40kXezo23sgF0cpzXM7neFPvyzC9R9Rn6KDw14tra8P+mv8fKnId2Nrz8Kanwe3vZN6HNs0e3GFDW/hz24wroB+G2VFODbsp27IkYRlBpc0OWYhtFQKjS2mZTDRUYAOHrE99hhr0b71yvb2HsP58t9B+LP9dy3Lu2yAghVgOoruBRCWHIiYVrMkEaAVnuCgSbMboFU+GA1I1fYcDs3uoYac4quqZszPEP0yKmqyI=~1";
    private readonly static string WizzairRequestVerificationToken = "4c21da4476114f7782203a8e5f530e85";
    private readonly static DateRange ArrivalDateRange = new(new DateTime(2023, 4, 1), new DateTime(2023, 5, 1));
    private readonly static DateRange ReturnDateRange = new(new DateTime(2023, 4, 1), new DateTime(2023, 5, 1));
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