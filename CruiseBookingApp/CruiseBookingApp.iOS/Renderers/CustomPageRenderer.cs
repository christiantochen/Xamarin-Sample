using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using CruiseBookingApp.iOS.Renderers;
using CruiseBookingApp.Utils;

[assembly: ExportRenderer(typeof(Page), typeof(CustomPageRenderer))]
namespace CruiseBookingApp.iOS.Renderers
{
    public class CustomPageRenderer : PageRenderer
    {
        IElementController ElementController => Element as IElementController;

        protected override void OnElementChanged(VisualElementChangedEventArgs e)
        {
            base.OnElementChanged(e);

            if (Element is Page page)
                NavigationPage.SetBackButtonTitle(page, "Back");
        }
    }
}
