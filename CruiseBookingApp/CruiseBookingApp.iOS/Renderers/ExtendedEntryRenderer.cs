using System;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using CoreAnimation;
using CoreGraphics;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using CruiseBookingApp.Controls;
using CruiseBookingApp.iOS.Renderers;

[assembly: ExportRenderer(typeof(ExtendedEntry), typeof(ExtendedEntryRenderer))]
namespace CruiseBookingApp.iOS.Renderers
{
    public class ExtendedEntryRenderer : EntryRenderer
    {
        public ExtendedEntry ExtendedElement => Element as ExtendedEntry;

        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (e.NewElement != null)
            {
                Control.BorderStyle = UITextBorderStyle.None;
                Control.TextColor = Element.TextColor.ToUIColor();

                SetReturnType();
                UpdateLineColor();
                UpdateCursorColor();

                Control.ShouldReturn += (UITextField tf) =>
                {
                    if (ExtendedElement?.ReturnType != ReturnType.Next)
                        ExtendedElement?.Unfocus();

                    ExtendedElement.InvokeCompleted();
                    return true;
                };
            }
        }

        public override void LayoutSubviews()
        {
            base.LayoutSubviews();

            LineLayer lineLayer = GetOrAddLineLayer();
            lineLayer.Frame = new CGRect(0, Frame.Size.Height - LineLayer.LineHeight, Control.Frame.Size.Width, LineLayer.LineHeight);
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (e.PropertyName.Equals(nameof(ExtendedEntry.LineColorToApply)))
            {
                UpdateLineColor();
            }
            else if (e.PropertyName.Equals(Entry.TextColorProperty.PropertyName))
            {
                UpdateCursorColor();
            }
            else if (e.PropertyName.Equals(ExtendedEntry.ReturnTypeProperty.PropertyName))
            {
                SetReturnType();
            }
        }

        void SetReturnType()
        {
            var type = ExtendedElement?.ReturnType;

            if (type == null)
                return;

            switch (type)
            {
                case ReturnType.Go:
                    Control.ReturnKeyType = UIReturnKeyType.Go;
                    break;
                case ReturnType.Next:
                    Control.ReturnKeyType = UIReturnKeyType.Next;
                    break;
                case ReturnType.Send:
                    Control.ReturnKeyType = UIReturnKeyType.Send;
                    break;
                case ReturnType.Search:
                    Control.ReturnKeyType = UIReturnKeyType.Search;
                    break;
                default:
                    Control.ReturnKeyType = UIReturnKeyType.Done;
                    break;
            }
        }

        void UpdateCursorColor()
        {
            Control.TintColor = Element.TextColor.ToUIColor();
        }

        void UpdateLineColor()
        {
            LineLayer lineLayer = GetOrAddLineLayer();
            lineLayer.BorderColor = ExtendedElement.LineColorToApply.ToCGColor();
        }

        LineLayer GetOrAddLineLayer()
        {
            var lineLayer = Control.Layer.Sublayers?.OfType<LineLayer>().FirstOrDefault();

            if (lineLayer == null)
            {
                lineLayer = new LineLayer();

                Control.Layer.AddSublayer(lineLayer);
                Control.Layer.MasksToBounds = true;
            }

            return lineLayer;
        }
    }
}
