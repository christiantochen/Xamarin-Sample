using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(CruiseBookingApp.Controls.KeyboardOverlappingBox), typeof(CruiseBookingApp.iOS.Renderers.KeyboardOverlappingBoxRenderer))]
namespace CruiseBookingApp.iOS.Renderers
{
    public class KeyboardOverlappingBoxRenderer : BoxRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<BoxView> e)
        {
            base.OnElementChanged(e);

            if (Element != null)
            {
                Element.HeightRequest = 0;
            }

            UIKeyboard.Notifications.ObserveWillShow((sender, args) =>
            {
                if (Element != null)
                {
                    Element.HeightRequest = args.FrameEnd.Height;
                }
            });

            UIKeyboard.Notifications.ObserveWillHide((sender, args) =>
            {
                if (Element != null)
                {
                    Element.HeightRequest = 0;
                }
            });
        }
    }
}