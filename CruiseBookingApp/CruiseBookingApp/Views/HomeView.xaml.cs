using Xamarin.Forms;
using CruiseBookingApp.ViewModels.Base;

namespace CruiseBookingApp.Views
{
    public partial class HomeView : ContentPage
    {
        public HomeView()
        {
            InitializeComponent();

            if (Device.RuntimePlatform == Device.Android)
            {
                CustomNavigationPage.SetIsTranslucent(this, true);
            }
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            if (BindingContext is IHandleViewAppearing viewAware)
            {
                await viewAware.OnViewAppearingAsync(this);
            }
        }

        protected override async void OnDisappearing()
        {
            base.OnDisappearing();

            if (BindingContext is IHandleViewDisappearing viewAware)
            {
                await viewAware.OnViewDisappearingAsync(this);
            }
        }
    }
}
