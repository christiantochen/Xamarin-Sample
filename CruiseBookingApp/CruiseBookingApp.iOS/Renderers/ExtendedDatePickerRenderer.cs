﻿using System;
using System.ComponentModel;
using System.Linq;
using CoreGraphics;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using CruiseBookingApp.Controls;
using CruiseBookingApp.iOS.Extensions;
using CruiseBookingApp.iOS.Renderers;

[assembly: ExportRenderer(typeof(ExtendedDatePicker), typeof(ExtendedDatePickerRenderer))]
namespace CruiseBookingApp.iOS.Renderers
{
    public class ExtendedDatePickerRenderer : DatePickerRenderer
    {
        ExtendedDatePicker ExtendedElement => Element as ExtendedDatePicker;

        protected override void OnElementChanged(ElementChangedEventArgs<DatePicker> e)
        {
            base.OnElementChanged(e);

            if (e.NewElement != null)
            {
                e.NewElement.HeightRequest = 32;

                Control.BorderStyle = UITextBorderStyle.None;
                Control.TextColor = Element.TextColor.ToUIColor();

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

        void UpdateLineColor()
        {
            LineLayer lineLayer = Control.GetOrAddLineLayer();
            lineLayer.BorderColor = ExtendedElement.LineColor.ToCGColor();
        }

        public override void LayoutSubviews()
        {
            base.LayoutSubviews();

            LineLayer lineLayer = Control.GetOrAddLineLayer();
            lineLayer.Frame = new CGRect(0, Frame.Size.Height - LineLayer.LineHeight, Control.Frame.Size.Width, LineLayer.LineHeight);
        }

        void UpdateFont()
        {
            Control.Font = UIFont.FromName(ExtendedElement.FontFamily, (nfloat)ExtendedElement.FontSize);
        }
    }
}
