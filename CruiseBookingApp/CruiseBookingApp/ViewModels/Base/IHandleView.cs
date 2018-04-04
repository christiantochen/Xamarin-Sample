using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace CruiseBookingApp.ViewModels.Base
{
    public interface IHandleViewAppearing
    {
        Task OnViewAppearingAsync(VisualElement view);
    }

    public interface IHandleViewDisappearing
    {
        Task OnViewDisappearingAsync(VisualElement view);
    }

    public interface IHandleExtendedListView
    {
        bool IsListViewReady { get; }
        Action<bool> EnableExtendedListViewScroll { get; set; }
    }
}
