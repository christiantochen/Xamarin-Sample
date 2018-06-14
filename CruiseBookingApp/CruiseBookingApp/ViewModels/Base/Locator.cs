using System;
using System.Collections.Generic;
using Autofac;
using CruiseBookingApp.PopupModels;
using CruiseBookingApp.Popups;
using CruiseBookingApp.Services.Authentication;
using CruiseBookingApp.Services.Cruise;
using CruiseBookingApp.Services.Dialog;
using CruiseBookingApp.Services.Navigation;
using CruiseBookingApp.Services.Port;
using CruiseBookingApp.Services.Promotion;
using CruiseBookingApp.Views;
using Xamarin.Forms;

namespace CruiseBookingApp.ViewModels.Base
{
    public class Locator
    {
        //private static Locator instance;
        private static Lazy<Locator> lazyInstance = new Lazy<Locator>(() => new Locator(), true);
        //public static Locator Instance => instance ?? (instance = new Locator());
        public static Locator Instance => lazyInstance.Value;

        private IContainer container;
        private readonly ContainerBuilder containerBuilder = new ContainerBuilder();
        private readonly Dictionary<Type, Type> mappings = new Dictionary<Type, Type>();

        private Locator()
        {
            RegisterAppViews();
            RegisterAppServices();
        }

        void RegisterAppViews()
        {
            // app
            RegisterView<AccountView, AccountViewModel>();
            RegisterView<BookingView, BookingViewModel>();
            RegisterView<BookingCruiseView, BookingCruiseViewModel>();
            RegisterView<BookingCruisesView, BookingCruisesViewModel>();
            RegisterView<HomeView, HomeViewModel>();
            RegisterView<LoginView, LoginViewModel>();
            RegisterView<MainView, MainViewModel>();
            // popup
            RegisterView<PickerPopup, PickerPopupModel>();
            RegisterView<TopMenuPopup, TopMenuPopupModel>();
            // default
            RegisterView<DefaultView, DefaultViewModel>();
        }

        void RegisterAppServices()
        {
            RegisterService<INavigationService, NavigationService>();
            RegisterService<IDialogService, DialogService>();

            if (AppSettings.UseFakes)
            {
                RegisterService<IAuthenticationService, FakeAuthenticationService>();
                RegisterService<ICruiseService, FakeCruiseService>();
                RegisterService<IPortService, FakePortService>();
                RegisterService<IPromotionService, FakePromotionService>();
            }
        }

        public T Resolve<T>() => container.Resolve<T>();

        public object Resolve(Type type) => container.Resolve(type);

        public void RegisterService<TInterface, TImplementation>() where TImplementation : TInterface
            => containerBuilder.RegisterType<TImplementation>().As<TInterface>();

        public void RegisterView<TView, TViewModel>()
            where TView : Page
            where TViewModel : ViewModelBase
        {
            containerBuilder.RegisterType<TViewModel>();
            mappings.Add(typeof(TViewModel), typeof(TView));
        }

        public void Build() => container = containerBuilder.Build();

        public Page GetView<TViewModel>() where TViewModel : ViewModelBase => GetView(typeof(TViewModel));

        public Page GetView(Type viewModelType)
        {
            var pageType = GetPageTypeForViewModel(viewModelType);

            if (pageType == null)
                throw new Exception($"Mapping type for {viewModelType} is not a page");

            var page = Activator.CreateInstance(pageType) as Page;
            var viewModel = Resolve(viewModelType) as ViewModelBase;
            page.BindingContext = viewModel;

            return page;
        }

        private Type GetPageTypeForViewModel(Type viewModelType)
        {
            if (!mappings.ContainsKey(viewModelType))
                throw new KeyNotFoundException($"No map for ${viewModelType} was found on mappings");

            return mappings[viewModelType];
        }

        private Type GetPageTypeForViewModel<TViewModel>() where TViewModel : ViewModelBase
        {
            return GetPageTypeForViewModel(typeof(TViewModel));
        }
    }
}
