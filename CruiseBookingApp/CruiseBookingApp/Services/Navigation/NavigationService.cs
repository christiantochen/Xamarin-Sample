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

        Dictionary<Type, Type> _mappings => Locator.Instance.Mappings;

        protected Application CurrentApplication => Application.Current;

        public NavigationService(IAuthenticationService authenticationService)
            => _authenticationService = authenticationService;

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

        public Task NavigateToAsync<TViewModel>() where TViewModel : ViewModelBase
        {
            return InternalNavigateToAsync(typeof(TViewModel), null);
        }

        public Task NavigateToAsync<TViewModel>(object parameter) where TViewModel : ViewModelBase
        {
            return InternalNavigateToAsync(typeof(TViewModel), parameter);
        }

        public Task NavigateToAsync(Type viewModelType)
        {
            return InternalNavigateToAsync(viewModelType, null);
        }

        public Task NavigateToAsync(Type viewModelType, object parameter)
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
            Page page = CreateAndBindPage(viewModelType, parameter);

            if (page is LoginView || page is MainView)
            {
                CurrentApplication.MainPage = page;
            }
            else if (CurrentApplication.MainPage is MainView mainView &&
                     mainView.CurrentPage is CustomNavigationPage navigationPage)
            {
                await navigationPage.PushAsync(page);
            }

            await (page.BindingContext as ViewModelBase).InitializeAsync(parameter);
        }

        protected Type GetPageTypeForViewModel(Type viewModelType)
        {
            if (!_mappings.ContainsKey(viewModelType))
            {
                throw new KeyNotFoundException($"No map for ${viewModelType} was found on navigation mappings");
            }

            return _mappings[viewModelType];
        }

        protected Page CreateAndBindPage(Type viewModelType, object parameter)
        {
            Type pageType = GetPageTypeForViewModel(viewModelType);

            if (pageType == null)
            {
                throw new Exception($"Mapping type for {viewModelType} is not a page");
            }

            Page page = Activator.CreateInstance(pageType) as Page;
            ViewModelBase viewModel = Locator.Instance.Resolve(viewModelType) as ViewModelBase;
            page.BindingContext = viewModel;

            return page;
        }
    }
}
