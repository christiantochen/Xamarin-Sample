using System.ComponentModel;
using Android.Content;
using Android.Graphics;
using Android.Graphics.Drawables;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using CruiseBookingApp.Controls;
using CruiseBookingApp.Droid.Extensions;
using CruiseBookingApp.Droid.Renderers;

[assembly: ExportRenderer(typeof(ExtendedDatePicker), typeof(ExtendedDatePickerRenderer))]
namespace CruiseBookingApp.Droid.Renderers
{
    public class ExtendedDatePickerRenderer : DatePickerRenderer
    {
        Typeface typeFace;
        ExtendedDatePicker ExtendedElement => Element as ExtendedDatePicker;

        public ExtendedDatePickerRenderer(Context context) : base(context) { }

        protected override void OnElementChanged(ElementChangedEventArgs<DatePicker> e)
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
                                                Android.Graphics.PorterDuff.Mode.SrcAtop);
        }
    }
}
