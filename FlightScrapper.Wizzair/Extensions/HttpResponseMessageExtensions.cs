using System.Net.Http;
using System.Threading.Tasks;

namespace FlightScrapper.Wizzair.Extensions
{
    public static class HttpResponseMessageExtensions
    {
        // Do same as EnsureSuccessStatusCode, but throw more meaningful exception
        public static async Task EnsureSuccess(this HttpResponseMessage message)
        {
            if (message.IsSuccessStatusCode)
            {
                return;
            }
            string content = null;
            if (message.Content != null)
            {
                content = await message.Content.ReadAsStringAsync();
                message.Content.Dispose();
            }

            var contentMessage = string.IsNullOrWhiteSpace(content) ? string.Empty : $" Content: {content}";
            string exceptionMessage = $"Response status code does not indicate success: {(int)message.StatusCode} ({message.ReasonPhrase}).{contentMessage}";
            throw new HttpRequestException(exceptionMessage);
        }
    }
}
