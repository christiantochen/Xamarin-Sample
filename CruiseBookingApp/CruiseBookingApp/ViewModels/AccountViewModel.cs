using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using CruiseBookingApp.Models;
using CruiseBookingApp.ViewModels.Base;

namespace CruiseBookingApp.ViewModels
{
    public class AccountViewModel : ViewModelBase, IHandleViewAppearing, IHandleViewDisappearing
    {
        public AccountViewModel()
        {
            Title = "Account";
        }

        public override Task InitializeAsync(object navigationData)
        {
            return base.InitializeAsync(navigationData);
        }

        public User User => AppSettings.User;

        public ICommand LogoutCommand => new AsyncCommand(Logout);

        Task Logout(object obj)
        {
            AppSettings.RemoveUserData();

            return NavigationService.NavigateToAsync<LoginViewModel>();
        }

        public Task OnViewAppearingAsync(VisualElement view)
        {
            return Task.FromResult(true);
        }

        public Task OnViewDisappearingAsync(VisualElement view)
        {
            return Task.FromResult(true);
        }
    }
}
