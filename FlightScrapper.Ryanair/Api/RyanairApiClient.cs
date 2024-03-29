﻿using FlightScrapper.Core.Extensions;
using FlightScrapper.Core.Services;
using FlightScrapper.Ryanair.Api.RequestModels;
using FlightScrapper.Ryanair.Api.ResponseModels.FlightAvailability;
using FlightScrapper.Ryanair.Api.ResponseModels.Routes;
using FlightScrapper.Ryanair.Utils;
using Polly;
using Polly.Retry;
using System.Net.Http;
using System.Net.Http.Json;

namespace FlightScrapper.Ryanair.Api
{
    internal class RyanairApiClient
    {
        private readonly AsyncRetryPolicy _retryPolicy;
        private HttpRequestMessage _requestTemplate;

        public RyanairApiClient(HttpRequestMessage requestTemplate)
        {
            _requestTemplate = requestTemplate;
            _retryPolicy = Policy.Handle<TimeoutException>().RetryAsync(3);
        }

        public async Task<FlightAvailabilityDto> GetFlightAvailability(FlightAvailabilityParametersDto parameters)
        {
            var httpClient = HttpClientFactory.CreateHttpClient();
            var request = CreateRequestFromTemplate();
            request.AddBrowserUserAgent();
            request.RequestUri = new Uri($"https://www.ryanair.com/api/booking/v4/pl-pl/availability{HttpUtils.ToQueryString(parameters)}");
            request.Method = HttpMethod.Get;

            HttpResponseMessage response = await _retryPolicy.ExecuteAsync(async () => await httpClient.SendAsync(request));
            await response.EnsureSuccess();
            return await response.Content.ReadFromJsonAsync<FlightAvailabilityDto>();
        }

        public async Task<IEnumerable<RouteDto>> GetRoutes(string originAirportCode)
        {
            var httpClient = HttpClientFactory.CreateHttpClient();
            var request = CreateRequestFromTemplate();
            request.AddBrowserUserAgent();
            request.RequestUri = new Uri($"https://www.ryanair.com/api/views/locate/searchWidget/routes/pl/airport/{originAirportCode}");
            request.Method = HttpMethod.Get;

            HttpResponseMessage response = await _retryPolicy.ExecuteAsync(async () => await httpClient.SendAsync(request));
            await response.EnsureSuccess();
            return await response.Content.ReadFromJsonAsync<IEnumerable<RouteDto>>();
        }

        public async Task<IEnumerable<AirportDto>> GetAirports()
        {
            var httpClient = HttpClientFactory.CreateHttpClient();
            var request = CreateRequestFromTemplate();
            request.AddBrowserUserAgent();
            request.RequestUri = new Uri($"https://www.ryanair.com/api/views/locate/5/airports/pl/active");
            request.Method = HttpMethod.Get;

            HttpResponseMessage response = await _retryPolicy.ExecuteAsync(async () => await httpClient.SendAsync(request));
            await response.EnsureSuccess();
            return await response.Content.ReadFromJsonAsync<IEnumerable<AirportDto>>();
        }

        private HttpRequestMessage CreateRequestFromTemplate()
        {
            var request = _requestTemplate.Clone();
            request.Content = null;

            return request;
        }
    }
}
