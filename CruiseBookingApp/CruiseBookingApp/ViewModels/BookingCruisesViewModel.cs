using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using CruiseBookingApp.Models;
using CruiseBookingApp.Services.Cruise;
using CruiseBookingApp.ViewModels.Base;

namespace CruiseBookingApp.ViewModels
{
    public class BookingCruisesViewModel : ViewModelBase, IHandleViewAppearing, IHandleViewDisappearing
    {
        ObservableCollection<CruiseGroup> _cruiseGroups = new ObservableCollection<CruiseGroup>();

        readonly ICruiseService _cruiseService;

        public BookingCruisesViewModel(ICruiseService cruiseService)
        {
            _cruiseService = cruiseService;
        }

        public ObservableCollection<CruiseGroup> CruiseGroups
        {
            get => _cruiseGroups;
            set
            {
                _cruiseGroups = value;
                OnPropertyChanged();
            }
        }

        public override async Task InitializeAsync(object navigationData)
        {
            if (navigationData is Dictionary<string, object> data)
            {
                var origin = data["originPort"] as Port;
                var destination = data["destinationPort"] as Port;
                var departureDate = data["departureDate"] as DateTime?;

                Title = $"{origin.Name} - {destination.Name}";

                IsBusy = true;

                try
                {
                    var results = await _cruiseService.GetCruisesAsync(origin, destination, departureDate ?? DateTime.Today);
                    results.ForEach((c) => CruiseGroups.Add(c));
                }
                catch (Exception ex)
                {

                }
                finally
                {
                    IsBusy = false;
                }
            }
        }

        public ICommand TappedCommand => new AsyncCommand(Tapped);

        public Task Tapped(object obj)
        {
            if (obj is Cruise cruise)
            {
                return NavigationService.NavigateToAsync<BookingCruiseViewModel>(cruise);
            }

            return Task.FromResult(true);
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
