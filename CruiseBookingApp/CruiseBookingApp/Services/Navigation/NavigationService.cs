using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;
using CruiseBookingApp.Services.Authentication;
using CruiseBookingApp.ViewModels;
using CruiseBookingApp.ViewModels.Base;
using CruiseBookingApp.Views;

namespace CruiseBookingApp.Services.Navigation
{
    public partial class NavigationService : INavigationService
    {
        IAuthenticationService _authenticationService;

        protected Application CurrentApplication => Application.Current;

        public NavigationService(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        public async Task InitializeAsync()
        {
            if (await _authenticationService.UserIsAuthenticatedAndValidAsync())
            {
                await NavigateToAsync<MainViewModel>();
            }
            else
            {
                await NavigateToAsync<LoginViewModel>();
            }
        }

        public Task NavigateToAsync<TViewModel>(object parameter = null) where TViewModel : ViewModelBase
        {
            return InternalNavigateToAsync(typeof(TViewModel), parameter);
        }

        public Task NavigateToAsync(Type viewModelType, object parameter = null)
        {
            return InternalNavigateToAsync(viewModelType, parameter);
        }

        public async Task NavigateBackAsync()
        {
            if (CurrentApplication.MainPage is CustomNavigationPage navigationPage)
            {
                await navigationPage.Navigation.PopAsync();
            }
        }

        public virtual Task RemoveLastFromBackStackAsync()
        {
            return Task.FromResult(true);
        }

        protected virtual async Task InternalNavigateToAsync(Type viewModelType, object parameter)
        {
            var view = Locator.Instance.GetView(viewModelType);

            if (view is LoginView || view is MainView)
            {
                CurrentApplication.MainPage = view;
            }
            else if (CurrentApplication.MainPage is MainView mainView &&
                     mainView.CurrentPage is CustomNavigationPage navigationPage)
            {
                await navigationPage.PushAsync(view);
            }

            await (view.BindingContext as ViewModelBase).InitializeAsync(parameter);
        }

    }
}
