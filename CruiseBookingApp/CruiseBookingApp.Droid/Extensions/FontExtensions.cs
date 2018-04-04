using System;

namespace CruiseBookingApp.Droid.Extensions
{
    public static class FontExtensions
    {
        public static string FontNameToFontFile(this string fontFamily)
        {
            int hashtagIndex = fontFamily.IndexOf('#');
            if (hashtagIndex >= 0)
                return fontFamily.Substring(0, hashtagIndex);

            throw new InvalidOperationException($"Can't parse the {nameof(fontFamily)} {fontFamily}");
        }
    }
}
