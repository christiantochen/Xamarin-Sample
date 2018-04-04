using System;
using CoreGraphics;
using UIKit;

namespace CruiseBookingApp.iOS.Extensions
{
    public static class UIImageExtensions
    {
        public static UIImage ToSolidColor(this UIImage imageSource, CGColor cgColor)
        {
            CGRect drawRect = new CGRect(0.0, 0.0, imageSource.Size.Width, imageSource.Size.Height);

            UIGraphics.BeginImageContextWithOptions(imageSource.Size, false, 0.0f);
            using (var context = UIGraphics.GetCurrentContext())
            {
                context.TranslateCTM(0, drawRect.Size.Height);
                context.ScaleCTM(1.0f, -1.0f);
                context.ClipToMask(drawRect, imageSource.CGImage);
                context.SetFillColor(cgColor);
                context.FillRect(drawRect);
                var tintedImage = UIGraphics.GetImageFromCurrentImageContext();
                UIGraphics.EndImageContext();
                return tintedImage;
            }
        }
    }
}
