using System;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Xamarin.Forms;
using CruiseBookingApp.Models;
using CruiseBookingApp.ViewModels.Base;

namespace CruiseBookingApp.ViewModels
{
    public class BookingCruiseViewModel : ViewModelBase, IHandleViewAppearing, IHandleViewDisappearing
    {
        Cruise _cruise;
        CruiseClass _selectedCruiseClass;
        double _totalBabyPassengers;
        double _totalMalePassengers;
        double _totalFemalePassengers;
        double _totalBabiesPrice;
        double _totalMalesPrice;
        double _totalFemalesPrice;
        double _totalPrice;

        public BookingCruiseViewModel() { }

        public Cruise Cruise
        {
            get => _cruise;
            set
            {
                _cruise = value;
                OnPropertyChanged();
            }
        }

        public CruiseClass SelectedCruiseClass
        {
            get => _selectedCruiseClass;
            set
            {
                _selectedCruiseClass = value;
                OnPropertyChanged();
            }
        }


        public double TotalBabyPassengers
        {
            get => _totalBabyPassengers;
            set
            {
                _totalBabyPassengers = value;
                OnPropertyChanged();
            }
        }

        public double TotalMalePassengers
        {
            get => _totalMalePassengers;
            set
            {
                _totalMalePassengers = value;
                OnPropertyChanged();
            }
        }

        public double TotalFemalePassengers
        {
            get => _totalFemalePassengers;
            set
            {
                _totalFemalePassengers = value;
                OnPropertyChanged();
            }
        }

        public double TotalBabiesPrice
        {
            get => _totalBabiesPrice;
            set
            {
                _totalBabiesPrice = value;
                OnPropertyChanged();
            }
        }

        public double TotalMalesPrice
        {
            get => _totalMalesPrice;
            set
            {
                _totalMalesPrice = value;
                OnPropertyChanged();
            }
        }

        public double TotalFemalesPrice
        {
            get => _totalFemalesPrice;
            set
            {
                _totalFemalesPrice = value;
                OnPropertyChanged();
            }
        }

        public double TotalPrice
        {
            get => _totalPrice;
            set
            {
                _totalPrice = value;
                OnPropertyChanged();
            }
        }
        void UpdateTotalPrice()
        {
            TotalMalesPrice = TotalMalePassengers * SelectedCruiseClass.PricePerAdultMale;
            TotalFemalesPrice = TotalFemalePassengers * SelectedCruiseClass.PricePerAdultFemale;
            TotalBabiesPrice = TotalBabyPassengers * SelectedCruiseClass.PricePerBaby;
            TotalPrice = TotalMalesPrice + TotalFemalesPrice + TotalBabiesPrice;
        }

        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            switch (propertyName)
            {
                case (nameof(SelectedCruiseClass)):
                case (nameof(TotalMalePassengers)):
                case (nameof(TotalFemalePassengers)):
                case (nameof(TotalBabyPassengers)):
                    UpdateTotalPrice();
                    break;
            }
        }

        public override Task InitializeAsync(object navigationData)
        {
            if (navigationData is Cruise cruise)
            {
                Cruise = cruise;
                SelectedCruiseClass = cruise.Classes.First();

                Title = "Booking Info";
            }

            return Task.FromResult(0);
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
