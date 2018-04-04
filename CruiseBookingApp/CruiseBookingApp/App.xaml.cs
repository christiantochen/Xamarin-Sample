using System.Linq;
using System.Threading.Tasks;
using Plugin.TextToSpeech;
using Plugin.TextToSpeech.Abstractions;
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
        public static ITextToSpeech TextToSpeech => CrossTextToSpeech.Current;

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

        protected override async void OnStart()
        {
            return;

            CrossLocale selectedCrossLocale;

            if (Device.RuntimePlatform == Device.Android)
            {
                var installedLanguages = await TextToSpeech.GetInstalledLanguages();
                selectedCrossLocale = installedLanguages.FirstOrDefault((c) => c.ToString() == "in-ID");
            }
            else
            {
                selectedCrossLocale = new CrossLocale { Language = "in-ID" };
            }

            await TextToSpeech.Speak("Selamat datang di aplikasi pelni", selectedCrossLocale);
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
