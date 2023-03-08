using FlightScrapper.Wizzair.Api.RequestModels.Timetable;
using FlightScrapper.Wizzair.Api.ResponseModels.Map;
using FlightScrapper.Wizzair.Api.ResponseModels.Timetable;
using FlightScrapper.Wizzair.Extensions;
using Polly.Retry;
using Polly;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;

namespace FlightScrapper.Ryanair.Api
{
    internal class WizzairApiClient
    {
        private readonly HttpClient _httpClient;
        private readonly string _wizzairCookie;
        private string _wizzairRequestVerificationToken;
        private readonly AsyncRetryPolicy _retryPolicy;

        public WizzairApiClient(string wizzairCookie, string wizzairRequestVerificationToken)
        {
            _httpClient = new HttpClient();
            _httpClient.Timeout = TimeSpan.FromSeconds(15);

            _wizzairCookie = wizzairCookie;
            _wizzairRequestVerificationToken = wizzairRequestVerificationToken;
            _retryPolicy = Policy.Handle<TimeoutException>().RetryAsync(2);
        }

        public async Task<MapDto> GetMap()
        {
            string url = "https://be.wizzair.com/16.1.0/Api/asset/map?languageCode=pl-pl";
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, url);
            request.Headers.Add("accept", "application/json, text/plain, */*");
            request.Headers.Add("cookie", _wizzairCookie);

            HttpResponseMessage response = await _retryPolicy.ExecuteAsync(async () => await _httpClient.SendAsync(request));
            await response.EnsureSuccess();
            return await response.Content.ReadFromJsonAsync<MapDto>();
        }

        public async Task<TimetableDto> GetTimetable(TimetableRequestDto timetableRequest)
        {
            string url = "https://be.wizzair.com/16.1.0/Api/search/timetable";
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, url);

            request.Headers.Add("accept", "application/json, text/plain, */*");
            request.Headers.Add("cookie", _wizzairCookie);
            request.Headers.Add("origin", "https://wizzair.com");
            request.Headers.Add("x-requestverificationtoken", _wizzairRequestVerificationToken);

            request.Content = new StringContent(JsonSerializer.Serialize(timetableRequest));
            request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json;charset=UTF-8");

            HttpResponseMessage response = await _retryPolicy.ExecuteAsync(async () => await _httpClient.SendAsync(request));
            await response.EnsureSuccess();
            var responseBody = await response.Content.ReadFromJsonAsync<TimetableDto>();
            return responseBody;
        }
    }
}
