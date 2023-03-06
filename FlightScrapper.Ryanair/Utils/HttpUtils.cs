using System.Web;

namespace FlightScrapper.Ryanair.Utils
{
    internal static class HttpUtils
    {
        public static string ToQueryString<T>(T obj)
        {
            var properties = from p in obj.GetType().GetProperties()
                             where p.GetValue(obj, null) != null
                             select p.Name.ToLower() + "=" + HttpUtility.UrlEncode(p.GetValue(obj, null).ToString());

            return "?" + string.Join("&", properties.ToArray());
        }
    }
}
