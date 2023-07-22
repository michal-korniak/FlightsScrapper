using System.Text;

namespace FlightScrapper.Core.Extensions
{
    public static class HttpRequestMessageExtensions
    {
        public static HttpRequestMessage Clone(this HttpRequestMessage req)
        {
            var clone = new HttpRequestMessage(req.Method, req.RequestUri)
            {
                Content = req.Content,
                Version = req.Version
            };

            foreach (var header in req.Headers)
                clone.Headers.TryAddWithoutValidation(header.Key, header.Value);

            return clone;
        }

        public static string ToCurlCommand(this HttpRequestMessage request)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));

            var stringBuilder = new StringBuilder();

            stringBuilder.AppendFormat("curl -X {0} \"{1}\"", request.Method, request.RequestUri);

            foreach (var header in request.Headers)
            {
                stringBuilder.AppendFormat(" -H \"{0}: {1}\"", header.Key, string.Join(", ", header.Value));
            }

            if (request.Content != null)
            {
                foreach (var header in request.Content.Headers)
                {
                    stringBuilder.AppendFormat(" -H \"{0}: {1}\"", header.Key, string.Join(", ", header.Value));
                }

                if (request.Content is StringContent || request.Content is ByteArrayContent)
                {
                    var result = request.Content is StringContent ? request.Content.ReadAsStringAsync().Result : Convert.ToBase64String(request.Content.ReadAsByteArrayAsync().Result);
                    stringBuilder.AppendFormat(" --data-binary \"{0}\"", result.Replace("\"", "\\\"")); // Escape double quotes
                }
                else if (request.Content is MultipartFormDataContent multipartContent)
                {
                    // This is a 'simplified' approach that does not consider non-binary data.
                    var boundary = multipartContent.Headers.GetValues("boundary").First();
                    var parts = new List<string>();

                    foreach (var content in multipartContent)
                    {
                        var partBuilder = new StringBuilder();
                        partBuilder.AppendFormat("--{0}\r\n", boundary);
                        partBuilder.Append(content.ReadAsStringAsync().Result);
                        partBuilder.Append("\r\n");
                        parts.Add(partBuilder.ToString());
                    }

                    parts.Add("--" + boundary + "--\r\n");

                    stringBuilder.Append(" --data-binary @- <<EOF\n");
                    stringBuilder.Append(string.Join("", parts));
                    stringBuilder.Append("EOF");
                }
            }

            return stringBuilder.ToString();
        }
    }
}
