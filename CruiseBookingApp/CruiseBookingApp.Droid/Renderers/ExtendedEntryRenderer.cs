using System;
using System.ComponentModel;
using Android.Content;
using Android.Views;
using Android.Views.InputMethods;
using Android.Widget;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using CruiseBookingApp.Controls;

[assembly: ExportRenderer(typeof(ExtendedEntry), typeof(CruiseBookingApp.Droid.Renderers.ExtendedEntryRenderer))]
namespace CruiseBookingApp.Droid.Renderers
{
    public class ExtendedEntryRenderer : EntryRenderer
    {
        public ExtendedEntry ExtendedElement => Element as ExtendedEntry;

        public ExtendedEntryRenderer(Context context) : base(context) => AutoPackage = false;

        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (e.NewElement != null)
            {
                Control.InputType |= Android.Text.InputTypes.TextFlagNoSuggestions;
                Control.SetTextColor(Element.TextColor.ToAndroid());

                UpdateLineColor();
                UpdateReturnType();

                // Editor Action is called when the return button is pressed
                Control.EditorAction += (object sender, TextView.EditorActionEventArgs args) =>
                {
                    if (ExtendedElement?.ReturnType != ReturnType.Next)
                    {
                        ExtendedElement?.Unfocus();
                    }

                    // Call all the methods attached to base_entry event handler Completed
                    ExtendedElement?.InvokeCompleted();
                };
            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            switch (e.PropertyName)
            {
                case nameof(ExtendedEntry.LineColorToApply):
                    UpdateLineColor();
                    break;
                case nameof(ExtendedEntry.InvalidColor):
                case nameof(ExtendedEntry.InvalidIcon):
                case nameof(ExtendedEntry.IsValidProperty):
                    UpdateValidity();
                    break;
            }
        }

        void UpdateValidity()
        {
            if (!ExtendedElement.IsValid)
            {
                var invalidIcon = Context.GetIconizeDrawable(ExtendedElement.InvalidIcon,
                                                             (Int32)ExtendedElement.HeightRequest,
                                                             ExtendedElement.InvalidColor);

                Control.SetCompoundDrawablesRelativeWithIntrinsicBounds(null, null, invalidIcon, null);
            }
            else
            {
                Control.SetCompoundDrawablesRelativeWithIntrinsicBounds(null, null, null, null);
            }
        }

        void UpdateLineColor()
        {
            Control?.Background?.SetColorFilter(ExtendedElement.LineColorToApply.ToAndroid(),
                                                Android.Graphics.PorterDuff.Mode.SrcAtop);
        }

        void UpdateReturnType()
        {
            var type = ExtendedElement?.ReturnType;

            if (!type.HasValue)
                return;

            switch (type.Value)
            {
                case ReturnType.Go:
                    Control.ImeOptions = ImeAction.Go;
                    break;
                case ReturnType.Next:
                    Control.ImeOptions = ImeAction.Next;
                    break;
                case ReturnType.Send:
                    Control.ImeOptions = ImeAction.Send;
                    break;
                case ReturnType.Search:
                    Control.ImeOptions = ImeAction.Search;
                    break;
                default:
                    Control.ImeOptions = ImeAction.Done;
                    break;
            }
        }
    }
}

