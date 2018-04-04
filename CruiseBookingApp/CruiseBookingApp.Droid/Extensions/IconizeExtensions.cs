using System;
using Android.Graphics.Drawables;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

namespace Android.Content
{
    public static class IconizeExtensions
    {
        /// <summary>
        /// Gets the drawable from file.
        /// </summary>
        /// <returns>The drawable.</returns>
        /// <param name="file">File.</param>
        public static Drawable GetIconizeDrawable(this Context context,
                             FileImageSource file,
                             Int32? iconSize = null,
                             Color? iconColor = null)
        {
            if (file == null)
                return null;

            Drawable drawableIcon;

            var icon = Plugin.Iconize.Iconize.FindIconForKey(file);
            if (icon != null)
            {
                var iconizeIcon = new Plugin.Iconize.Droid.Controls.IconDrawable(context, file);

                if (iconColor.HasValue)
                    iconizeIcon = iconizeIcon.Color(iconColor.Value.ToAndroid());

                if (iconSize.HasValue)
                    iconizeIcon = iconizeIcon.SizeDp(iconSize.Value);

                drawableIcon = iconizeIcon;
            }
            else drawableIcon = context.GetDrawable(file);

            return drawableIcon;
        }
    }
}
