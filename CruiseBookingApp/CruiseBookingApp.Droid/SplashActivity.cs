using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;

namespace CruiseBookingApp.Droid
{
    [Activity(
        Theme = "@style/SplashTheme",
        MainLauncher = false,
        NoHistory = true,
        ScreenOrientation = ScreenOrientation.Portrait)]
    public class SplashActivity : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.SplashScreen);

            if (Build.VERSION.SdkInt >= BuildVersionCodes.Lollipop)
            {
                Window.DecorView.SystemUiVisibility = StatusBarVisibility.Visible;
                Window.SetStatusBarColor(Android.Graphics.Color.Transparent);
            }
        }

        // Launches the startup task
        protected override void OnResume()
        {
            base.OnResume();
            Task startupWork = new Task(() => { SimulateStartup(); });
            startupWork.Start();
        }

        // Simulates background work that happens behind the splash screen
        async void SimulateStartup()
        {
            await Task.Delay(3000); // Simulate a bit of startup work.
            StartActivity(new Intent(Application.Context, typeof(MainActivity)));
        }

        public override void OnWindowFocusChanged(bool hasFocus)
        {
            base.OnWindowFocusChanged(hasFocus);

            ImageView imageView = FindViewById<ImageView>(Resource.Id.animated_loading);

            Android.Graphics.Drawables.AnimationDrawable animation = (Android.Graphics.Drawables.AnimationDrawable)imageView.Drawable;

            animation.Start();
        }
    }
}
