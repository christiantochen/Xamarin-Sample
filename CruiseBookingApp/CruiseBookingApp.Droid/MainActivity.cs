using Acr.UserDialogs;
using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Util;
using Android.Views;
using CarouselView.FormsPlugin.Android;
using CruiseBookingApp.Helpers;
using FFImageLoading.Forms.Droid;
using FormsPlugin.Iconize.Droid;
using Plugin.Iconize;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

namespace CruiseBookingApp.Droid
{
    [Activity(MainLauncher = true,
              LaunchMode = LaunchMode.SingleTop,
              ScreenOrientation = ScreenOrientation.Portrait)]
    public class MainActivity : FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            Forms.Init(this, savedInstanceState);
            CarouselViewRenderer.Init();
            CachedImageRenderer.Init(true);
            UserDialogs.Init(this);

            Iconize.With(new Plugin.Iconize.Fonts.MaterialModule());
            IconControls.Init(Resource.Id.toolbar, Resource.Id.sliding_tabs);

            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            //MakeStatusBarTranslucent(true);
            InitMessageCenterSubscriptions();
            LoadApplication(new App());
        }

        void InitMessageCenterSubscriptions()
        {
            MessagingCenter.Instance.Subscribe<StatusBarHelper, bool>(
                this,
                StatusBarHelper.TranslucentStatusChangeMessage,
                (helper, makeTranslucent) => MakeStatusBarTranslucent(makeTranslucent));
        }

        void MakeStatusBarTranslucent(bool makeTranslucent)
        {
            if (makeTranslucent)
            {
                SetStatusBarColor(Android.Graphics.Color.Transparent);

                if (Build.VERSION.SdkInt >= BuildVersionCodes.Lollipop)
                {
                    Window.DecorView.SystemUiVisibility = (StatusBarVisibility)(SystemUiFlags.LayoutFullscreen
                                                                                | SystemUiFlags.LayoutStable);
                    //| SystemUiFlags.LightStatusBar);
                }
            }
            else
            {
                using (var value = new TypedValue())
                {
                    if (Theme.ResolveAttribute(Resource.Attribute.colorPrimaryDark, value, true))
                    {
                        var color = new Android.Graphics.Color(value.Data);
                        SetStatusBarColor(color);
                    }
                }

                if (Build.VERSION.SdkInt >= BuildVersionCodes.Lollipop)
                {
                    Window.DecorView.SystemUiVisibility = StatusBarVisibility.Visible;
                }
            }
        }
    }
}
