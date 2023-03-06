﻿using FlightScrapper.Wizzair.Api.RequestModels.Timetable;
using FlightScrapper.Wizzair.Api.ResponseModels.Map;
using FlightScrapper.Wizzair.Api.ResponseModels.Timetable;
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
        public WizzairApiClient(string wizzairCookie)
        {
            _wizzairCookie = wizzairCookie;
            _httpClient = new HttpClient();
        }

        public async Task<MapDto> GetMap()
        {
            string url = "https://be.wizzair.com/16.1.0/Api/asset/map?languageCode=pl-pl";
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, url);
            request.Headers.Add("accept", "application/json, text/plain, */*");
            request.Headers.Add("cookie", _wizzairCookie);

            HttpResponseMessage response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<MapDto>();
        }

        public async Task<TimetableDto> GetTimetable(TimetableRequestDto timetableRequest)
        {
            string url = "https://be.wizzair.com/16.1.0/Api/search/timetable";
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, url);

            request.Headers.Add("accept", "application/json, text/plain, */*");
            request.Headers.Add("cookie", _wizzairCookie);
            request.Headers.Add("origin", "https://wizzair.com");
            request.Headers.Add("x-requestverificationtoken", "9ace4c35881246e1bc43977c92199a42");

            request.Content = new StringContent(JsonSerializer.Serialize(timetableRequest));
            request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json;charset=UTF-8");

            HttpResponseMessage response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();
            var responseBody = await response.Content.ReadFromJsonAsync<TimetableDto>();
            return responseBody;
        }
    }
}