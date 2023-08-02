using FlightScrapper.Wizzair.Api.RequestModels.Timetable;
using FlightScrapper.Wizzair.Api.ResponseModels.Map;
using FlightScrapper.Wizzair.Api.ResponseModels.Timetable;
using Polly.Retry;
using Polly;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using FlightScrapper.Core.Extensions;
using FlightScrapper.Core.Services;

namespace FlightScrapper.Wizzair.Api
{
    internal class WizzairApiClient
    {
        private readonly AsyncRetryPolicy _retryPolicy;
        private readonly HttpRequestMessage _requestTemplate;
        private string _wizzairApiVersion => _requestTemplate.RequestUri.LocalPath.Split('/')[1];

        public WizzairApiClient(HttpRequestMessage requestTemplate)
        {
            _retryPolicy = Policy.Handle<TimeoutException>().WaitAndRetryAsync(5, retryNumber => TimeSpan.FromSeconds(retryNumber * 3));
            _requestTemplate = requestTemplate;
        }

        public async Task<MapDto> GetMap()
        {
            var httpClient = HttpClientFactory.CreateHttpClient(180);
            var request = CreateRequestFromTemplate();
            request.AddBrowserUserAgent();
            request.AddKeepAliveHeader();
            request.RequestUri = new Uri($"https://be.wizzair.com/{_wizzairApiVersion}/Api/asset/map?languageCode=pl-pl");
            request.Method = HttpMethod.Get;

            HttpResponseMessage response = await _retryPolicy.ExecuteAsync(async () => await httpClient.SendAsync(request));
            await response.EnsureSuccess();
            return await response.Content.ReadFromJsonAsync<MapDto>();
        }

        public async Task<TimetableDto> GetTimetable(TimetableRequestDto timetableRequest)
        {
            var httpClient = HttpClientFactory.CreateHttpClient(180);
            var request = CreateRequestFromTemplate();
            request.AddBrowserUserAgent();
            request.AddKeepAliveHeader();
            request.RequestUri = new Uri($"https://be.wizzair.com/{_wizzairApiVersion}/Api/search/timetable");
            request.Method = HttpMethod.Post;
            request.Content = new StringContent(JsonSerializer.Serialize(timetableRequest));
            request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json;charset=UTF-8");

            HttpResponseMessage response = await _retryPolicy.ExecuteAsync(async () => await httpClient.SendAsync(request));
            await response.EnsureSuccess();
            var responseBody = await response.Content.ReadFromJsonAsync<TimetableDto>();
            return responseBody;
        }


        private HttpRequestMessage CreateRequestFromTemplate()
        {
            var request = _requestTemplate.Clone();
            request.Content = null;

            return request;
        }
    }
}
