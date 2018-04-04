using Xamarin.Forms;
using CruiseBookingApp.Helpers;

namespace CruiseBookingApp.Views
{
    public partial class LoginView : ContentPage
    {
        public LoginView()
        {
            NavigationPage.SetHasNavigationBar(this, false);

            InitializeComponent();

            UsernameEntry.Completed += (sender, e) => PasswordEntry.Focus();
        }
    }
}
