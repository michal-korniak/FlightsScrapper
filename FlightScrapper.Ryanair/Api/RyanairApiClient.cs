using FlightScrapper.Ryanair.Api.RequestModels;
using FlightScrapper.Ryanair.Api.ResponseModels.FlightAvailability;
using FlightScrapper.Ryanair.Api.ResponseModels.Routes;
using FlightScrapper.Ryanair.Utils;
using Polly;
using Polly.Retry;
using System.Net.Http.Json;

namespace FlightScrapper.Ryanair.Api
{
    internal class RyanairApiClient
    {
        private readonly HttpClient _httpClient;
        private readonly AsyncRetryPolicy _retryPolicy;

        public RyanairApiClient()
        {
            _httpClient = new HttpClient();
            _httpClient.Timeout = TimeSpan.FromSeconds(15);
            _retryPolicy = Policy.Handle<TimeoutException>().RetryAsync(2);
        }

        public async Task<FlightAvailabilityDto> GetFlightAvailability(FlightAvailabilityParametersDto parameters)
        {
            var queryString = HttpUtils.ToQueryString(parameters);
            var url = $"https://www.ryanair.com/api/booking/v4/pl-pl/availability{queryString}";

            var response = await _retryPolicy.ExecuteAsync(async () => await _httpClient.GetFromJsonAsync<FlightAvailabilityDto>(url));
            return response;
        }

        public async Task<IEnumerable<RouteDto>> GetRoutes(string originAirportCode)
        {
            var url = $"https://www.ryanair.com/api/views/locate/searchWidget/routes/pl/airport/{originAirportCode}";

            var response = await _retryPolicy.ExecuteAsync(async () => await _httpClient.GetFromJsonAsync<IEnumerable<RouteDto>>(url));
            return response;
        }

        public async Task<IEnumerable<AirportDto>> GetAirports()
        {
            var url = $"https://www.ryanair.com/api/views/locate/5/airports/pl/active";

            var response = await _retryPolicy.ExecuteAsync(async () => await _httpClient.GetFromJsonAsync<IEnumerable<AirportDto>>(url));
            return response;
        }
    }
}
