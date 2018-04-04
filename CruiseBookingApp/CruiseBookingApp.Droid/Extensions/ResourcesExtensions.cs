using System;
using Android.Content.Res;

namespace Android.Content.Res
{
    public static class ResourcesExtensions
    {
        public static int GetStatusbarHeight(this Resources resources)
        {
            var statusbarHeightId = resources.GetIdentifier("status_bar_height", "dimen", "android");

            if (statusbarHeightId > 0)
            {
                return resources.GetDimensionPixelSize(statusbarHeightId);
            }

            return 0;
        }
    }
}
