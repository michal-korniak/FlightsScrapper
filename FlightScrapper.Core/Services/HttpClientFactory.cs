using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace FlightScrapper.Core.Services
{

    public static class HttpClientFactory
    {
        private static IHost _host;
        private static string _clientName = "main";

        static HttpClientFactory()
        {
            var builder = new HostBuilder()
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddHttpClient(_clientName).ConfigurePrimaryHttpMessageHandler(messageHandler =>
                    {
                        var handler = new HttpClientHandler();
                        if (handler.SupportsAutomaticDecompression)
                        {
                            handler.AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip;
                        }
                        return handler;
                    });
                }).UseConsoleLifetime();

            _host = builder.Build();
        }

        public static HttpClient CreateHttpClient(int timeoutInSeconds = 30)
        {
            using var serviceScope = _host.Services.CreateScope();
            var services = serviceScope.ServiceProvider;
            var factory = services.GetRequiredService<IHttpClientFactory>();
            var httpClient = factory.CreateClient(_clientName);
            httpClient.Timeout = TimeSpan.FromSeconds(timeoutInSeconds);
            return httpClient;
        }
    }
}
