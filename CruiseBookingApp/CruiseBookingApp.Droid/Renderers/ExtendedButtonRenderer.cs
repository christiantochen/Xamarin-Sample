using System;
using System.Collections.Generic;
using System.ComponentModel;
using Android.Content;
using Android.Graphics.Drawables;
using Android.Util;
using Android.Views;
using Plugin.Iconize.Droid.Controls;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using CruiseBookingApp.Controls;

[assembly: ExportRenderer(typeof(ExtendedButton), typeof(CruiseBookingApp.Droid.Renderers.ExtendedButtonRenderer))]
namespace CruiseBookingApp.Droid.Renderers
{
    /// <summary>
    /// Defines the <see cref="ExtendedButtonRenderer" /> renderer.
    /// </summary>
    /// <seealso cref="Xamarin.Forms.Platform.Android.AppCompat.ButtonRenderer" />
    public class ExtendedButtonRenderer : ButtonRenderer
    {
        ExtendedButton ExtendedElement => Element as ExtendedButton;

        public ExtendedButtonRenderer(Context context) : base(context) { }

        protected override void OnElementChanged(ElementChangedEventArgs<Button> e)
        {
            base.OnElementChanged(e);

            if (e.NewElement != null)
            {
                UpdateIcon();
                UpdatePadding();
                UpdateTextAlignment();
            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            switch (e.PropertyName)
            {
                case nameof(ExtendedElement.Icon):
                case nameof(ExtendedElement.IconSize):
                case nameof(ExtendedElement.IconColor):
                case nameof(ExtendedElement.IconOrientation):
                case nameof(ExtendedElement.IconSpacing):
                    UpdateIcon();
                    break;
                case nameof(ExtendedElement.Padding):
                    UpdatePadding();
                    break;
                case nameof(ExtendedElement.TextAlignment):
                    UpdateTextAlignment();
                    break;
            }
        }

        void UpdateTextAlignment()
        {
            var flag = GravityFlags.CenterHorizontal;

            switch (ExtendedElement.TextAlignment)
            {
                case Xamarin.Forms.TextAlignment.Start:
                    flag = GravityFlags.Left;
                    break;
                case Xamarin.Forms.TextAlignment.End:
                    flag = GravityFlags.Right;
                    break;
            }

            Control.Gravity = flag | GravityFlags.CenterVertical;
        }

        /// <summary>
        /// Updates the icon.
        /// </summary>
        void UpdateIcon()
        {
            Control.CompoundDrawablePadding = (int)ExtendedElement.IconSpacing;

            var icon = Context.GetIconizeDrawable(ExtendedElement.Icon,
                                                  ExtendedElement.IconSize,
                                                  ExtendedElement.IconColor);

            switch (ExtendedElement.IconOrientation)
            {
                case Button.ButtonContentLayout.ImagePosition.Left:
                    Control.SetCompoundDrawablesRelativeWithIntrinsicBounds(icon, null, null, null);
                    break;
                case Button.ButtonContentLayout.ImagePosition.Top:
                    Control.SetCompoundDrawablesRelativeWithIntrinsicBounds(null, icon, null, null);
                    break;
                case Button.ButtonContentLayout.ImagePosition.Right:
                    Control.SetCompoundDrawablesRelativeWithIntrinsicBounds(null, null, icon, null);
                    break;
                case Button.ButtonContentLayout.ImagePosition.Bottom:
                    Control.SetCompoundDrawablesRelativeWithIntrinsicBounds(null, null, null, icon);
                    break;
            }
        }

        /// <summary>
        /// Updates the padding.
        /// </summary>
        void UpdatePadding()
        {
            Control.SetPadding(
                (int)TypedValue.ApplyDimension(ComplexUnitType.Dip, (float)ExtendedElement.Padding.Left, Context.Resources.DisplayMetrics),
                (int)TypedValue.ApplyDimension(ComplexUnitType.Dip, (float)ExtendedElement.Padding.Top, Context.Resources.DisplayMetrics),
                (int)TypedValue.ApplyDimension(ComplexUnitType.Dip, (float)ExtendedElement.Padding.Right, Context.Resources.DisplayMetrics),
                (int)TypedValue.ApplyDimension(ComplexUnitType.Dip, (float)ExtendedElement.Padding.Bottom, Context.Resources.DisplayMetrics));
        }
    }
}

