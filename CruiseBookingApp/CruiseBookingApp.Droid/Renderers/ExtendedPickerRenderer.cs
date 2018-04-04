using System.ComponentModel;
using Android.Content;
using Android.Graphics;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using CruiseBookingApp.Controls;
using CruiseBookingApp.Droid.Extensions;
using CruiseBookingApp.Droid.Renderers;

[assembly: ExportRenderer(typeof(ExtendedPicker), typeof(ExtendedPickerRenderer))]
namespace CruiseBookingApp.Droid.Renderers
{
    public class ExtendedPickerRenderer : Xamarin.Forms.Platform.Android.AppCompat.PickerRenderer
    {
        Typeface typeFace;
        ExtendedPicker ExtendedElement => Element as ExtendedPicker;

        public ExtendedPickerRenderer(Context context) : base(context) { }

        protected override void OnElementChanged(ElementChangedEventArgs<Picker> e)
        {
            base.OnElementChanged(e);

            if (e.NewElement != null)
            {
                UpdateFont();
                UpdateLineColor();
            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            switch (e.PropertyName)
            {
                case nameof(ExtendedDatePicker.LineColor):
                    UpdateLineColor();
                    break;
                case nameof(ExtendedDatePicker.FontSize):
                case nameof(ExtendedDatePicker.FontFamily):
                    UpdateFont();
                    break;
            }
        }

        void UpdateFont()
        {
            typeFace = Typeface.CreateFromAsset(Context.Assets, ExtendedElement.FontFamily.FontNameToFontFile());
            Control.TextSize = (float)ExtendedElement.FontSize;
            Control.SetTypeface(typeFace, TypefaceStyle.Normal);
        }

        void UpdateLineColor()
        {
            Control?.Background?.SetColorFilter(ExtendedElement.LineColor.ToAndroid(),
                                                PorterDuff.Mode.SrcAtop);
        }
    }
}
