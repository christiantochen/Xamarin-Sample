using System.Linq;
using System.Threading.Tasks;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;
using CruiseBookingApp.Controls;
using CruiseBookingApp.Services.Dialog;
using CruiseBookingApp.Services.Navigation;

namespace CruiseBookingApp.ViewModels.Base
{
    public class ViewModelBase : BindableObject
    {
        bool _isBusy = true;
        string _title;

        protected readonly IDialogService DialogService;
        protected readonly INavigationService NavigationService;

        public ViewModelBase()
        {
            DialogService = Locator.Instance.Resolve<IDialogService>();
            NavigationService = Locator.Instance.Resolve<INavigationService>();
        }

        public bool IsBusy
        {
            get => _isBusy;
            set
            {
                _isBusy = value;
                OnPropertyChanged();
            }
        }

        public string Title
        {
            get => _title;
            set
            {
                _title = value;
                OnPropertyChanged();
            }
        }

        public virtual Task InitializeAsync(object navigationData)
        {
            return Task.FromResult(false);
        }
    }
}
