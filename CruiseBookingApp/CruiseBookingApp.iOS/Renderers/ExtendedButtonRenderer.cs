using System;
using System.ComponentModel;
using CoreGraphics;
using Plugin.Iconize.iOS;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using CruiseBookingApp.Controls;
using CruiseBookingApp.iOS.Extensions;

[assembly: ExportRenderer(typeof(ExtendedButton), typeof(CruiseBookingApp.iOS.Renderers.ExtendedButtonRenderer))]
namespace CruiseBookingApp.iOS.Renderers
{
    public class ExtendedButtonRenderer : ButtonRenderer
    {
        ExtendedButton ExtendedElement => Element as ExtendedButton;
        Thickness Padding => ExtendedElement.Padding;

        protected override void OnElementChanged(ElementChangedEventArgs<Button> e)
        {
            base.OnElementChanged(e);

            if (e.NewElement != null)
            {
                UpdateIcon();
                UpdateIconOrientation();
                UpdatePadding();
            }
        }

        public override void LayoutSubviews()
        {
            base.LayoutSubviews();

            if (ExtendedElement.IconOrientation == Button.ButtonContentLayout.ImagePosition.Right)
            {
                var imageInsets = new UIEdgeInsets(0, Control.Frame.Size.Width - (nfloat)Padding.Left - ExtendedElement.IconSize, 0, 0);
                Control.ImageEdgeInsets = imageInsets;
            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            switch (e.PropertyName)
            {
                case nameof(ExtendedElement.Icon):
                case nameof(ExtendedElement.IconColor):
                    UpdateIcon();
                    break;
                case nameof(ExtendedElement.IconSize):
                    UpdateIcon();
                    UpdateIconOrientation();
                    break;
                case nameof(ExtendedElement.IconOrientation):
                case nameof(ExtendedElement.Padding):
                    UpdateIconOrientation();
                    UpdatePadding();
                    break;
            }
        }

        void UpdateIcon()
        {
            var file = ExtendedElement.Icon;
            var iconColor = ExtendedElement.IconColor;
            var iconSize = ExtendedElement.IconSize;

            if (file == null)
            {
                Control.SetImage(null, UIControlState.Normal);
                ClearEdgeInsets();
                return;
            }

            var icon = Plugin.Iconize.Iconize.FindIconForKey(file);

            if (icon != null)
            {
                using (var image = icon.ToUIImage(iconSize > 0 ? iconSize :
                                                  (nfloat)ExtendedElement.HeightRequest))
                {
                    var tintedImage = image.ToSolidColor(iconColor.ToCGColor());

                    Control.SetImage(tintedImage.ImageWithRenderingMode(UIImageRenderingMode.AlwaysOriginal), UIControlState.Normal);
                }
            }
            else
            {
                using (var image = UIImage.FromBundle(file))
                {
                    var tintedImage = image.ToSolidColor(iconColor.ToCGColor());

                    Control.SetImage(tintedImage.ImageWithRenderingMode(UIImageRenderingMode.AlwaysOriginal), UIControlState.Normal);
                }
            }

            if (iconSize > 0)
            {
                Control.ImageView.ContentMode = iconSize > 0 ? UIViewContentMode.Center : UIViewContentMode.ScaleAspectFit;
            }
        }

        void UpdateIconOrientation()
        {
            // TODO: RESTORE ALIGNMENT TO DEFAULT
            if (ExtendedElement.Icon == null)
                return;

            // TODO: NEED ALOT OF REWORK
            switch (ExtendedElement.IconOrientation)
            {
                case Button.ButtonContentLayout.ImagePosition.Left:
                    AlignToLeft();
                    break;
                case Button.ButtonContentLayout.ImagePosition.Right:
                    AlignToRight();
                    break;
                case Button.ButtonContentLayout.ImagePosition.Top:
                    AlignToTop();
                    break;
                case Button.ButtonContentLayout.ImagePosition.Bottom:
                    AlignToBottom();
                    break;
            }
        }

        void ClearEdgeInsets()
        {
            Control.ImageEdgeInsets = new UIEdgeInsets(0, 0, 0, 0);
            Control.TitleEdgeInsets = new UIEdgeInsets(0, 0, 0, 0);
            Control.ContentEdgeInsets = new UIEdgeInsets(0, 0, 0, 0);
        }

        void UpdatePadding()
        {
            Control.ContentEdgeInsets = new UIEdgeInsets(0, (nfloat)Padding.Left, 0, (nfloat)Padding.Right);
        }

        void AlignToLeft()
        {
            var iconSize = ExtendedElement.IconSize;

            Control.HorizontalAlignment = UIControlContentHorizontalAlignment.Left;
            Control.TitleLabel.TextAlignment = UITextAlignment.Left;

            var titleInsets = new UIEdgeInsets(0, (nfloat)Element.ContentLayout.Spacing, 0, -1 * (nfloat)Element.ContentLayout.Spacing);
            Control.TitleEdgeInsets = titleInsets;
        }

        void AlignToRight()
        {
            var iconSize = ExtendedElement.IconSize;

            Control.HorizontalAlignment = UIControlContentHorizontalAlignment.Left;
            Control.TitleLabel.TextAlignment = UITextAlignment.Left;

            var titleInsets = new UIEdgeInsets(0, -iconSize, 0, -((nfloat)Element.ContentLayout.Spacing - iconSize));
            Control.TitleEdgeInsets = titleInsets;

            var imageInsets = new UIEdgeInsets(0, Control.IntrinsicContentSize.Width - (nfloat)Element.ContentLayout.Spacing - iconSize, 0, -1 * iconSize);
            Control.ImageEdgeInsets = imageInsets;
        }

        void AlignToTop()
        {
            var iconSize = ExtendedElement.IconSize;

            Control.VerticalAlignment = UIControlContentVerticalAlignment.Top;
            Control.HorizontalAlignment = UIControlContentHorizontalAlignment.Center;
            Control.TitleLabel.TextAlignment = UITextAlignment.Center;
            Control.TitleLabel.LineBreakMode = UILineBreakMode.WordWrap;
            Control.SizeToFit();

            CGSize titleSize = Control.TitleLabel.Frame.Size;

            Control.TitleEdgeInsets = new UIEdgeInsets(iconSize, Convert.ToInt32(-1 * iconSize / 2), -1 * iconSize, Convert.ToInt32(iconSize / 2)); ;
            Control.ImageEdgeInsets = new UIEdgeInsets(0, Convert.ToInt32(Control.IntrinsicContentSize.Width / 2 - iconSize / 2 - titleSize.Width / 2), 0, Convert.ToInt32(-1 * (Control.IntrinsicContentSize.Width / 2 - iconSize / 2 + titleSize.Width / 2))); ;
        }

        void AlignToBottom()
        {
            var iconSize = ExtendedElement.IconSize;

            Control.VerticalAlignment = UIControlContentVerticalAlignment.Bottom;
            Control.HorizontalAlignment = UIControlContentHorizontalAlignment.Center;
            Control.TitleLabel.TextAlignment = UITextAlignment.Center;
            Control.SizeToFit();
            Control.TitleEdgeInsets = new UIEdgeInsets(-1 * iconSize, -1 * iconSize, iconSize, iconSize);
            Control.ImageEdgeInsets = new UIEdgeInsets(0, 0, 0, 0);
        }
    }
}
