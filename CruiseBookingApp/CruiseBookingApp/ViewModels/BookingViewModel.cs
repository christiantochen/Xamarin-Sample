using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using CruiseBookingApp.Extensions;
using CruiseBookingApp.Models;
using CruiseBookingApp.Services.Port;
using CruiseBookingApp.ViewModels.Base;

namespace CruiseBookingApp.ViewModels
{
    public class BookingViewModel : ViewModelBase, IHandleViewAppearing, IHandleViewDisappearing
    {
        ObservableCollection<Models.Port> _ports = new ObservableCollection<Models.Port>();

        readonly IPortService _portService;

        public BookingViewModel(IPortService portService)
        {
            _portService = portService;

            Title = "Booking";
        }

        public ObservableCollection<Port> Ports
        {
            get => _ports;
            set
            {
                _ports = value;
                OnPropertyChanged();
            }
        }

        public override async Task InitializeAsync(object navigationData)
        {
            IsBusy = true;

            try
            {
                var result = await _portService.GetPortsAsync();

                Ports = result.ToObservableCollection();

                SelectedOriginPort = SelectedDestinationPort
                    = result.FirstOrDefault();

                OnPropertyChanged(nameof(SelectedOriginPort));
                OnPropertyChanged(nameof(SelectedDestinationPort));
            }
            catch (Exception ex)
            {
                DialogService.ShowToast(ex.Message);
            }
            finally
            {
                IsBusy = false;
            }
        }

        public Port SelectedOriginPort { get; set; }
        public Port SelectedDestinationPort { get; set; }
        public DateTime? SelectedDepartureDate { get; set; } = DateTime.Today;

        public ICommand SearchCommand => new AsyncCommand(Search);

        async Task Search()
        {
            if (SelectedOriginPort == null ||
                SelectedDestinationPort == null)
                return;

            var navigationParameter = new Dictionary<string, object>
            {
                { "departureDate", SelectedDepartureDate },
                { "originPort", SelectedOriginPort },
                { "destinationPort", SelectedDestinationPort },
            };

            await NavigationService.NavigateToAsync<BookingCruisesViewModel>(navigationParameter);
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
