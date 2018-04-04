using Xamarin.Forms;
using CruiseBookingApp.ViewModels.Base;

namespace CruiseBookingApp.Views
{
    public partial class AccountView : ContentPage
    {
        public AccountView()
        {
            InitializeComponent();

            NavigationPage.SetHasNavigationBar(this, false);

            Xamarin.Forms.PlatformConfiguration.iOSSpecific.Page.SetUseSafeArea(this, true);
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
