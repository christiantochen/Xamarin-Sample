using System;
using System.Threading.Tasks;
using CruiseBookingApp.ViewModels.Base;

namespace CruiseBookingApp.PopupModels
{
    public class PickerPopupModel : ViewModelBase
    {
        public PickerPopupModel() { }

        public string _title;
        public string Title
        {
            get => _title;
            set
            {
                _title = value;
                OnPropertyChanged();
            }
        }

        public override Task InitializeAsync(object navigationData)
        {
            if (navigationData is string title)
                Title = title;

            return base.InitializeAsync(navigationData);
        }
    }
}
