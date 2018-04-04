using CarouselView.FormsPlugin.iOS;
using Corcav.Behaviors;
using FFImageLoading.Forms.Touch;
using FormsPlugin.Iconize.iOS;
using Foundation;
using Plugin.Iconize;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

namespace CruiseBookingApp.iOS
{
    [Register("AppDelegate")]
    public partial class AppDelegate : FormsApplicationDelegate
    {
        public override bool FinishedLaunching(UIApplication uiApplication, NSDictionary launchOptions)
        {
            Forms.Init();
            CachedImageRenderer.Init();
            CarouselViewRenderer.Init();
            Infrastructure.Init();

            Iconize.With(new Plugin.Iconize.Fonts.MaterialModule());
            IconControls.Init();

            InitPlugins();

            ConfigureUI();
            LoadApplication(new App());

            return base.FinishedLaunching(uiApplication, launchOptions);
        }

        static void InitPlugins()
        {
            var t1 = typeof(CarouselView.FormsPlugin.Abstractions.CarouselViewControl);
            var t2 = typeof(Xamanimation.AnimationBase);
            var t3 = typeof(FFImageLoading.Transformations.TintTransformation);

            var bb = new IconImageRenderer();
        }

        static void ConfigureUI()
        {
            var titleAttribute = new UITextAttributes
            {
                Font = UIFont.FromName("Roboto-Regular", 14)
            };

            UIBarButtonItem.Appearance.SetTitleTextAttributes(titleAttribute, UIControlState.Normal);
            UINavigationBar.Appearance.SetTitleTextAttributes(titleAttribute);
            UITabBar.Appearance.SelectedImageTintColor = Color.FromHex("#FF5050").ToUIColor();

            UIView statusBar = UIApplication.SharedApplication.ValueForKey(new NSString("statusBar")) as UIView;
            statusBar.BackgroundColor = Color.FromHex("#FF5050").ToUIColor();
        }
    }
}
