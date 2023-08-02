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
            HttpRequestMessage request = new HttpRequestMessage();

            string[] fetchArgs = requestText.TrimStart("fetch(").TrimEnd(");").SplitByFirst(',');
            string url = fetchArgs[0].Trim().Trim('"');
            request.RequestUri = new Uri(url);

            if (fetchArgs.Length > 1)
            {
                JObject options = JObject.Parse(fetchArgs[1].Trim());

                string method = (string)options["method"];
                if (!string.IsNullOrEmpty(method))
                {
                    request.Method = new HttpMethod(method);
                }

                JObject headers = (JObject)options["headers"];
                if (headers != null)
                {
                    foreach (var header in headers)
                    {
                        string headerValue = ((string)header.Value).Trim();
                        request.Headers.TryAddWithoutValidation(header.Key, headerValue);
                    }
                }

                string body = (string)options["body"];
                if (body != null)
                {
                    request.Content = new StringContent(body);
                }
            }

            return request;
        }

    }


}
