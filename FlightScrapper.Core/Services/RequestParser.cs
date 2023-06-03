using FlightScrapper.Core.Extensions;
using Newtonsoft.Json.Linq;
using System.Text;

namespace FlightScrapper.Core.Services
{
    public static class RequestParser
    {
        public static async Task<HttpRequestMessage> ParseFromFile(string filePath)
        {
            string fileContent = await File.ReadAllTextAsync(filePath);
            return Parse(fileContent);
        }

        public static HttpRequestMessage Parse(string requestText)
        {
            HttpRequestMessage httpRequestMessage = new HttpRequestMessage();

            string[] fetchArgs = requestText.TrimStart("fetch(").TrimEnd(");").SplitByFirst(',');
            string url = fetchArgs[0].Trim().Trim('"');
            httpRequestMessage.RequestUri = new Uri(url);

            if (fetchArgs.Length > 1)
            {
                JObject options = JObject.Parse(fetchArgs[1].Trim());

                string method = (string)options["method"];
                if (!string.IsNullOrEmpty(method))
                {
                    httpRequestMessage.Method = new HttpMethod(method);
                }

                JObject headers = (JObject)options["headers"];
                if (headers != null)
                {
                    foreach (var header in headers)
                    {
                        httpRequestMessage.Headers.TryAddWithoutValidation(header.Key, (string)header.Value);
                    }
                }

                string body = (string)options["body"];
                if (body != null)
                {
                    httpRequestMessage.Content = new StringContent(body);
                }
            }

            return httpRequestMessage;
        }

    }


}
