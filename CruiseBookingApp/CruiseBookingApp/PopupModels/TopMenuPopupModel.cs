using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;
using CruiseBookingApp.ViewModels;
using CruiseBookingApp.ViewModels.Base;

namespace CruiseBookingApp.PopupModels
{
    public class TopMenuPopupModel : ViewModelBase
    {
        public ICommand LogoutCommand => new AsyncCommand(Logout);

        Task Logout(object obj)
        {
            App.Settings.RemoveUserData();

            return Task.WhenAll(
                NavigationService.NavigateToAsync<LoginViewModel>(),
                PopupNavigation.Instance.PopAsync()
            );
        }
    }
}
