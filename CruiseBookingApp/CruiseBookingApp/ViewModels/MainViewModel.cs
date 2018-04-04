using System;
using System.Threading.Tasks;
using CruiseBookingApp.ViewModels.Base;

namespace CruiseBookingApp.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        AccountViewModel _accountViewModel;
        DefaultViewModel _historyViewModel;
        HomeViewModel _homeViewModel;
        DefaultViewModel _reservationViewModel;

        public MainViewModel(HomeViewModel homeViewModel
                             , AccountViewModel accountViewModel
                             , DefaultViewModel defaultViewModel)
        {
            _homeViewModel = homeViewModel;
            _reservationViewModel = _historyViewModel = defaultViewModel;
            _accountViewModel = accountViewModel;
        }

        public HomeViewModel HomeViewModel
        {
            get => _homeViewModel;
            set
            {
                _homeViewModel = value;
                OnPropertyChanged();
            }
        }

        public DefaultViewModel ReservationViewModel
        {
            get => _reservationViewModel;
            set
            {
                _reservationViewModel = value;
                OnPropertyChanged();
            }
        }

        public DefaultViewModel HistoryViewModel
        {
            get => _historyViewModel;
            set
            {
                _historyViewModel = value;
                OnPropertyChanged();
            }
        }

        public AccountViewModel AccountViewModel
        {
            get => _accountViewModel;
            set
            {
                _accountViewModel = value;
                OnPropertyChanged();
            }
        }

        public override Task InitializeAsync(object navigationData)
        {
            return Task.WhenAll
                (
                    _homeViewModel.InitializeAsync(navigationData)
                );
        }
    }
}
