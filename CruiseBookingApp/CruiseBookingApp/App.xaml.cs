using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;
using CruiseBookingApp.Services.Device;
using CruiseBookingApp.Services.Native;
using CruiseBookingApp.Services.Navigation;
using CruiseBookingApp.ViewModels.Base;

namespace CruiseBookingApp
{
    public partial class App : Application
    {
        public static IDeviceService DeviceService => DependencyService.Get<IDeviceService>();
        public static INativeService NativeService => DependencyService.Get<INativeService>();

        static App() => BuildDependencies();

        Task InitializeNavigation() => Locator.Instance.Resolve<INavigationService>().InitializeAsync();

        public App()
        {
            InitializeComponent();
            InitializeNavigation();
        }

        static void BuildDependencies()
        {
            AppSettings.UseFakes = true;

            Locator.Instance.Build();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
