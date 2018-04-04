using System;

namespace CruiseBookingApp.Extensions
{
    public static class StringExtensions
    {
        public static bool IsNullOrWhiteSpace(this string @string) => string.IsNullOrWhiteSpace(@string);
        public static bool IsNullOrEmpty(this string @string) => string.IsNullOrEmpty(@string);
    }
}
