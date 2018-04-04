using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace CruiseBookingApp.Views
{
    public class MainView : TabbedPage
    {
        public MainView()
        {
            var listOfView = new List<Page>();

            var homeView = new HomeView { Title = "Home", Icon = "md-home" };
            homeView.SetBinding(BindingContextProperty, "HomeViewModel");

            var reservationView = new DefaultView { Title = "Reservation", Icon = "md-picture-in-picture-alt" };
            reservationView.SetBinding(BindingContextProperty, "ReservationViewModel");

            var historyView = new DefaultView { Title = "History", Icon = "md-history" };
            historyView.SetBinding(BindingContextProperty, "HistoryViewModel");

            var accountView = new AccountView { Title = "Account", Icon = "md-person" };
            accountView.SetBinding(BindingContextProperty, "AccountViewModel");

            listOfView.Add(homeView);
            listOfView.Add(reservationView);
            listOfView.Add(historyView);
            listOfView.Add(accountView);

            listOfView.Take(1).ForEach((view) => Children.Add(new CustomNavigationPage(view)
            {
                Title = view.Title,
                Icon = view.Icon
            }));

            listOfView.Skip(1).ForEach((view) => Children.Add(new NavigationPage(view)
            {
                Title = view.Title,
                Icon = view.Icon
            }));
        }
    }
}