using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using CruiseBookingApp.Extensions;
using CruiseBookingApp.Models;
using CruiseBookingApp.PopupModels;
using CruiseBookingApp.Services.Promotion;
using CruiseBookingApp.ViewModels.Base;

namespace CruiseBookingApp.ViewModels
{
    public class HomeViewModel : ViewModelBase, IHandleViewAppearing, IHandleViewDisappearing, IHandleExtendedListView
    {
        ObservableCollection<string> _firstViewBannerItems = new ObservableCollection<string>();
        ObservableCollection<FirstViewCategory> _firstViewCategoryItems = new ObservableCollection<FirstViewCategory>();

        readonly IPromotionService _promotionService;

        public HomeViewModel(IPromotionService promotionService)
        {
            _promotionService = promotionService;

            Title = "Home";
        }

        public ObservableCollection<string> FirstViewBannerItems
        {
            get => _firstViewBannerItems;
            set
            {
                _firstViewBannerItems = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<FirstViewCategory> FirstViewCategoryItems
        {
            get => _firstViewCategoryItems;
            set
            {
                _firstViewCategoryItems = value;
                OnPropertyChanged();
            }
        }

        public override async Task InitializeAsync(object navigationData)
        {
            IsBusy = true;
            IsListViewReady = false;

            try
            {
                var firstViewBannerItemResults = await _promotionService.GetFirstViewBannerItemsAsync();

                FirstViewBannerItems = firstViewBannerItemResults.ToObservableCollection();

                var firstViewCategoryItemResults = await _promotionService.GetFirstViewCategoryItemsAsync();

                FirstViewCategoryItems = firstViewCategoryItemResults.ToObservableCollection();
            }
            catch (Exception ex)
            {
                DialogService.ShowToast(ex.Message);
            }
            finally
            {
                IsBusy = false;
                IsListViewReady = true;
                EnableExtendedListViewScroll?.Invoke(true);
            }
        }

        public Action<bool> EnableExtendedListViewScroll { get; set; }
        public bool IsListViewReady { get; private set; } = true;

        public ICommand BookACruiseCommand => new AsyncCommand(BookACruise);
        public ICommand OnGoingPromosCommand => new AsyncCommand(OnGoingPromos);
        public ICommand ContactUsCommand => new AsyncCommand(ContactUs);
        public ICommand InformationCommand => new AsyncCommand(Information);
        public ICommand SecondaryMenuCommand => new AsyncCommand(SecondaryMenu);

        async Task BookACruise() => await NavigationService.NavigateToAsync<BookingViewModel>();
        async Task OnGoingPromos() => await NavigationService.NavigateToAsync<DefaultViewModel>();
        async Task ContactUs() => await NavigationService.NavigateToAsync<DefaultViewModel>();
        async Task Information() => await NavigationService.NavigateToAsync<DefaultViewModel>();
        async Task SecondaryMenu() => await NavigationService.NavigateToPopupAsync<TopMenuPopupModel>(true);

        public Task OnViewAppearingAsync(VisualElement view)
        {
            if (IsListViewReady)
                EnableExtendedListViewScroll?.Invoke(true);

            return Task.FromResult(true);
        }

        public Task OnViewDisappearingAsync(VisualElement view)
        {
            EnableExtendedListViewScroll?.Invoke(false);

            return Task.FromResult(true);
        }
    }
}
