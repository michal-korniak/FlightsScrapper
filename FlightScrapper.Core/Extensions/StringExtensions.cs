
namespace FlightScrapper.Core.Extensions
{
    public static class StringExtensions
    {
        public static string TrimStart(this string str, string trimmedValue)
        {
            if(string.IsNullOrEmpty(str) || string.IsNullOrEmpty(trimmedValue))
            {
                return str;
            }

            if(str.StartsWith(trimmedValue))
            {
                str = str[trimmedValue.Length..];
            }

            return str;
        }

        public static string TrimEnd(this string str, string trimmedValue)
        {
            if(string.IsNullOrEmpty(str) || string.IsNullOrEmpty(trimmedValue))
            {
                return str;
            }

            if(str.EndsWith(trimmedValue))
            {
                str = str[..^trimmedValue.Length];
            }

            return str;
        }

        public static string[] SplitByFirst(this string str, char separator)
        {
            if (string.IsNullOrEmpty(str))
            {
                return new[] { str };
            }

            var separatorIndex = str.IndexOf(separator);

            if (separatorIndex == -1) // No separator found
            {
                return new[] { str };
            }

            var firstPart = str.Substring(0, separatorIndex);
            var secondPart = str.Substring(separatorIndex + 1); // "+ 1" to not include the separator itself in the second part

            return new[] { firstPart, secondPart };
        }
    }

}
