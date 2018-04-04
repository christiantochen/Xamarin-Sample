using System;
using FormsPlugin.Iconize.iOS;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using CruiseBookingApp.iOS.Extensions;
using CruiseBookingApp.iOS.Renderers;

[assembly: ExportRenderer(typeof(TabbedPage), typeof(MainViewRenderer))]
namespace CruiseBookingApp.iOS.Renderers
{
    public class MainViewRenderer : IconTabbedPageRenderer
    {
        protected override void OnElementChanged(VisualElementChangedEventArgs e)
        {
            base.OnElementChanged(e);
        }
    }
}
