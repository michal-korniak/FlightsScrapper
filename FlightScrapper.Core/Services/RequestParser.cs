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
            var lines = requestText.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);

            // Parse the request line
            var requestLineParts = lines[0].Split(' ');
            if (requestLineParts.Length < 3)
                throw new ArgumentException("Invalid request line");

            HttpMethod method;
            switch (requestLineParts[0].ToUpper())
            {
                case "GET":
                    method = HttpMethod.Get;
                    break;
                case "POST":
                    method = HttpMethod.Post;
                    break;
                case "PUT":
                    method = HttpMethod.Put;
                    break;
                case "DELETE":
                    method = HttpMethod.Delete;
                    break;
                default:
                    throw new ArgumentException("Invalid HTTP method");
            }

            var requestUri = requestLineParts[1];

            var request = new HttpRequestMessage(method, requestUri);

            // Parse the headers
            foreach (var line in lines.Skip(1))
            {
                if (string.IsNullOrWhiteSpace(line)) break; // End of headers

                var parts = line.Split(new[] { ": " }, 2, StringSplitOptions.None);
                if (parts.Length < 2) throw new ArgumentException($"Invalid header line: {line}");

                request.Headers.TryAddWithoutValidation(parts[0], parts[1]);
            }

            return request;
        }
    }


}
