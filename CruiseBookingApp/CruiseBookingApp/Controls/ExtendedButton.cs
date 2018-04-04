using System;
using Xamarin.Forms;

namespace CruiseBookingApp.Controls
{
    public class ExtendedButton : Button
    {
        const double defaultAndroidMarginHorizontalSize = -4;
        const double defaultAndroidMarginVerticalSize = -6;

        public ExtendedButton()
        {
#if __ANDROID__
            // HACK: ANDROID BUTTON HAS MARGINLIKE RIPPLE SPACE AROUND 4px
            Margin = new Thickness(defaultAndroidMarginHorizontalSize, defaultAndroidMarginVerticalSize);
#endif
        }

        public static readonly BindableProperty PaddingProperty =
            BindableProperty.Create("Padding", typeof(Thickness), typeof(ExtendedButton), new Thickness(0));

        public Thickness Padding
        {
            get { return (Thickness)GetValue(PaddingProperty); }
            set { SetValue(PaddingProperty, value); }
        }

        public static readonly BindableProperty TextAlignmentProperty =
            BindableProperty.Create("TextAlignment", typeof(TextAlignment), typeof(ExtendedButton), TextAlignment.Center);

        public TextAlignment TextAlignment
        {
            get { return (TextAlignment)GetValue(TextAlignmentProperty); }
            set { SetValue(TextAlignmentProperty, value); }
        }

        public static readonly BindableProperty IconSizeProperty =
            BindableProperty.Create("IconSize", typeof(int), typeof(ExtendedButton), 24);

        public int IconSize
        {
            get { return (int)GetValue(IconSizeProperty); }
            set { SetValue(IconSizeProperty, value); }
        }

        public static readonly BindableProperty IconColorProperty =
            BindableProperty.Create("IconColor", typeof(Color), typeof(ExtendedButton), Color.Default);

        public Color IconColor
        {
            get { return (Color)GetValue(IconColorProperty); }
            set { SetValue(IconColorProperty, value); }
        }

        public static readonly BindableProperty IconProperty =
            BindableProperty.Create("Icon", typeof(FileImageSource), typeof(ExtendedButton), null);

        public FileImageSource Icon
        {
            get { return (FileImageSource)GetValue(IconProperty); }
            set { SetValue(IconProperty, value); }
        }

        public static readonly BindableProperty IconOrientationProperty =
            BindableProperty.Create("IconOrientation", typeof(ButtonContentLayout.ImagePosition), typeof(ExtendedButton), ButtonContentLayout.ImagePosition.Left);

        public ButtonContentLayout.ImagePosition IconOrientation
        {
            get { return (ButtonContentLayout.ImagePosition)GetValue(IconOrientationProperty); }
            set { SetValue(IconOrientationProperty, value); }
        }

        public static readonly BindableProperty IconSpacingProperty =
            BindableProperty.Create("IconSpacing", typeof(int), typeof(ExtendedButton), 10);

        public int IconSpacing
        {
            get { return (int)GetValue(IconSpacingProperty); }
            set { SetValue(IconSpacingProperty, value); }
        }

        protected override void OnPropertyChanging(string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

#if __ANDROID__
            if (propertyName == nameof(TextAlignment))
            {
                if (TextAlignment == TextAlignment.Start ||
                    TextAlignment == TextAlignment.End)
                {
                    // HACK: IsDefault margin then reset it to 0
                    if (Margin == new Thickness(-4, -6))
                        Margin = new Thickness(0);
                }
            }
#endif
        }
    }
}

