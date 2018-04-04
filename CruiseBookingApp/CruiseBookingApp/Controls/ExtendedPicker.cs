using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Xamarin.Forms;

namespace CruiseBookingApp.Controls
{
    public class ExtendedPicker : Picker
    {
        public ExtendedPicker() { }

        public static readonly BindableProperty LineColorProperty =
            BindableProperty.Create("LineColor", typeof(Color), typeof(ExtendedDatePicker), Color.Default);

        public Color LineColor
        {
            get { return (Color)GetValue(LineColorProperty); }
            set { SetValue(LineColorProperty, value); }
        }

        public static readonly BindableProperty FontFamilyProperty =
            BindableProperty.Create("FontFamily", typeof(string), typeof(ExtendedPicker), Font.Default.FontFamily);

        public string FontFamily
        {
            get { return (string)GetValue(FontFamilyProperty); }
            set { SetValue(FontFamilyProperty, value); }
        }

        public static readonly BindableProperty FontSizeProperty =
            BindableProperty.Create("FontSize", typeof(double), typeof(ExtendedPicker), Font.Default.FontSize);

        public double FontSize
        {
            get { return (double)GetValue(FontSizeProperty); }
            set { SetValue(FontSizeProperty, value); }
        }
    }
}