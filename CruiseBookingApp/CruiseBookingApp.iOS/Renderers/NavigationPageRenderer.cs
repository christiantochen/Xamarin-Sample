using Xamarin.Forms.Platform.iOS;
using System.ComponentModel;
using Xamarin.Forms;
using UIKit;
using CruiseBookingApp.iOS.Renderers;
using CruiseBookingApp.Views;
using System.Linq;

[assembly: ExportRenderer(typeof(NavigationPage), typeof(NavigationPageRenderer))]
namespace CruiseBookingApp.iOS.Renderers
{
    public class NavigationPageRenderer : NavigationRenderer
    {
        INavigationPageController ElementController => Element as INavigationPageController;

        protected override void OnElementChanged(VisualElementChangedEventArgs e)
        {
            base.OnElementChanged(e);

        }
    }
}
