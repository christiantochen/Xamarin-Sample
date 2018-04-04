using Xamarin.Forms;

namespace CruiseBookingApp.Controls
{
    public class ExtendedListView : ListView
    {
        public ExtendedListView() : base(ListViewCachingStrategy.RecycleElementAndDataTemplate) { }
    }
}
