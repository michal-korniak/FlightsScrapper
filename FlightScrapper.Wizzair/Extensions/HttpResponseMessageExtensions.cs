using System.Net.Http;
using System.Threading.Tasks;

namespace FlightScrapper.Wizzair.Extensions
{
    public class DetailedHttpRequestException: HttpRequestException
    {
        public int Code { get; }
        public override string Message { get; }

        public DetailedHttpRequestException(int code, string message)
        {
            Code = code;
            Message = message;
        }
    }

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

            throw new DetailedHttpRequestException((int)message.StatusCode, content);
        }
    }
}
