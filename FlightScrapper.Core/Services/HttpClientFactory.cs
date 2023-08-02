
using System.Net;

namespace FlightScrapper.Core.Services
{
    public static class HttpClientFactory
    {
        public static HttpClient CreateHttpClient(int timeoutInSeconds = 30)
        {
            var clientHandler = new HttpClientHandler() { AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate };
            var httpClient = new HttpClient(clientHandler);
            httpClient.Timeout = TimeSpan.FromSeconds(timeoutInSeconds);

            return httpClient;
        }
    }
}
