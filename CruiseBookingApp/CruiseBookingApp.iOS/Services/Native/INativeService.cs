using CruiseBookingApp.iOS.Services.Native;
using CruiseBookingApp.Services.Native;

[assembly: Xamarin.Forms.Dependency(typeof(NativeService))]
namespace CruiseBookingApp.iOS.Services.Native
{
    public class NativeService : INativeService
    {

    }
}
