using Xamarin.Forms;

namespace CruiseBookingApp.Views
{
    public partial class BookingView : ContentPage
    {
        public BookingView()
        {
            InitializeComponent();

            if (Device.RuntimePlatform == Device.Android)
            {
                CustomNavigationPage.SetIsTranslucent(this, false);
                CustomNavigationPage.SetBarBackgroundColorDefault();
            }
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
        }
    }
}
