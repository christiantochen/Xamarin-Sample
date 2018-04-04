using Android.App;
using Plugin.CurrentActivity;
using Xamarin.Forms;
using CruiseBookingApp.Droid.Services.Native;
using CruiseBookingApp.Services.Native;

[assembly: Dependency(typeof(NativeService))]
namespace CruiseBookingApp.Droid.Services.Native
{
    public class NativeService : INativeService
    {
        Activity Activity = CrossCurrentActivity.Current.Activity;
    }
}
