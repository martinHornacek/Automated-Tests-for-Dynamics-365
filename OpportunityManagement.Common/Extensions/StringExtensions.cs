using System.Globalization;

namespace OpportunityManagement.Common.Extensions
{
    public static class StringExtensions
    {
        public static string FormatWith(this string str, params object[] values)
        {
            return string.IsNullOrWhiteSpace(str) ? string.Empty : string.Format(CultureInfo.InvariantCulture, str, values);
        }

        public static bool IsLowercase(this string str)
        {
            if (string.IsNullOrEmpty(str)) return false;
            return str.ToLower() == str;
        }

        public static int? ToNullableInt(this string str)
        {
            return int.TryParse(str, out var result) ? (int?)result : null;
        }
    }
}