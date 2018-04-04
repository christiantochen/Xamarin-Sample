using Xamarin.Forms;
using System;
using FormsPlugin.Iconize;

namespace CruiseBookingApp.Views
{
    public partial class CustomNavigationPage : NavigationPage
    {
        static string _defaultFontFamily => Device.RuntimePlatform == Device.Android ? "Fonts/Roboto-Regular.ttf#Roboto" : "Roboto-Regular";
        static double _defaultFontSize => Device.RuntimePlatform == Device.Android ? 18d : 16d;

        public CustomNavigationPage(Page root) : base(root)
        {
            Popped += OnNavigation;
            PoppedToRoot += OnNavigation;
            Pushed += OnNavigation;
        }

        public static readonly BindableProperty FontFamilyProperty = BindableProperty.CreateAttached("FontFamily", typeof(string), typeof(CustomNavigationPage), _defaultFontFamily);
        public static readonly BindableProperty FontSizeProperty = BindableProperty.CreateAttached("FontSize", typeof(double), typeof(CustomNavigationPage), _defaultFontSize);
        public static readonly BindableProperty IsTranslucentProperty = BindableProperty.CreateAttached("IsTranslucent", typeof(bool), typeof(CustomNavigationPage), false);

        public static string GetFontFamily(BindableObject view)
        {
            return (string)view.GetValue(FontFamilyProperty);
        }

        public static double GetFontSize(BindableObject view)
        {
            return (double)view.GetValue(FontSizeProperty);
        }

        public static bool GetIsTranslucent(BindableObject view)
        {
            return (bool)view.GetValue(IsTranslucentProperty);
        }

        public static void SetFontFamily(BindableObject view, object value)
        {
            view.SetValue(FontFamilyProperty, value);
        }

        public static void SetFontSize(BindableObject view, double value)
        {
            view.SetValue(FontSizeProperty, value);
        }

        public static void SetIsTranslucent(BindableObject view, bool value)
        {
            view.SetValue(IsTranslucentProperty, value);
        }

        public static Color GetBarBackgroundColor()
        {
            if (Application.Current.MainPage is MainView mainView &&
               mainView.CurrentPage is CustomNavigationPage customNavigationPage)
            {
                return customNavigationPage.BarBackgroundColor;
            }

            return Color.Default;
        }

        public static void SetBarBackgroundColor(Color value)
        {
            if (Application.Current.MainPage is MainView mainView &&
               mainView.CurrentPage is CustomNavigationPage customNavigationPage)
            {
                customNavigationPage.BarBackgroundColor = value;
            }
        }

        public static void SetBarBackgroundColorDefault()
        {
            SetBarBackgroundColor((Color)Application.Current.Resources["NavigationBarBackgroundColor"]);
        }

        /// <summary>
        /// Called when [navigation].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="NavigationEventArgs"/> instance containing the event data.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        void OnNavigation(Object sender, NavigationEventArgs e)
        {
            MessagingCenter.Send(sender, IconToolbarItem.UpdateToolbarItemsMessage);
        }
    }
}