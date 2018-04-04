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

namespace CruiseBookingApp.ViewModels.Base
{
    public class Locator
    {
        IContainer _container;
        ContainerBuilder _containerBuilder;

        static readonly Locator _instance = new Locator();

        public static Locator Instance
        {
            get
            {
                return _instance;
            }
        }

        readonly Dictionary<Type, Type> _mappings;
        public Dictionary<Type, Type> Mappings => _mappings;

        public Locator()
        {
            _mappings = new Dictionary<Type, Type>();
            CreatePageViewModelMappings();

            _containerBuilder = new ContainerBuilder();
            CreateAppContainerBuilder();
        }

        void CreatePageViewModelMappings()
        {
            _mappings.Add(typeof(AccountViewModel), typeof(AccountView));
            _mappings.Add(typeof(BookingViewModel), typeof(BookingView));
            _mappings.Add(typeof(BookingCruiseViewModel), typeof(BookingCruiseView));
            _mappings.Add(typeof(BookingCruisesViewModel), typeof(BookingCruisesView));
            _mappings.Add(typeof(HomeViewModel), typeof(HomeView));
            _mappings.Add(typeof(LoginViewModel), typeof(LoginView));
            _mappings.Add(typeof(MainViewModel), typeof(MainView));

            _mappings.Add(typeof(PickerPopupModel), typeof(PickerPopup));
            _mappings.Add(typeof(TopMenuPopupModel), typeof(TopMenuPopup));

            _mappings.Add(typeof(DefaultViewModel), typeof(DefaultView));
        }

        void CreateAppContainerBuilder()
        {
            // Services
            _containerBuilder.RegisterType<NavigationService>().As<INavigationService>();
            _containerBuilder.RegisterType<DialogService>().As<IDialogService>();

            if (AppSettings.UseFakes)
            {
                _containerBuilder.RegisterType<FakeAuthenticationService>().As<IAuthenticationService>();
                _containerBuilder.RegisterType<FakeCruiseService>().As<ICruiseService>();
                _containerBuilder.RegisterType<FakePortService>().As<IPortService>();
                _containerBuilder.RegisterType<FakePromotionService>().As<IPromotionService>();
            }

            // ViewModels
            _containerBuilder.RegisterType<AccountViewModel>();
            _containerBuilder.RegisterType<BookingViewModel>();
            _containerBuilder.RegisterType<BookingCruiseViewModel>();
            _containerBuilder.RegisterType<BookingCruisesViewModel>();
            _containerBuilder.RegisterType<HomeViewModel>();
            _containerBuilder.RegisterType<LoginViewModel>();
            _containerBuilder.RegisterType<MainViewModel>();

            _containerBuilder.RegisterType<PickerPopupModel>();
            _containerBuilder.RegisterType<TopMenuPopupModel>();

            _containerBuilder.RegisterType<DefaultViewModel>();
        }

        public T Resolve<T>() => _container.Resolve<T>();

        public object Resolve(Type type) => _container.Resolve(type);

        public void Register<TInterface, TImplementation>() where TImplementation : TInterface
            => _containerBuilder.RegisterType<TImplementation>().As<TInterface>();

        public void Register<T>() where T : class => _containerBuilder.RegisterType<T>();

        public void Build() => _container = _containerBuilder.Build();
    }
}
