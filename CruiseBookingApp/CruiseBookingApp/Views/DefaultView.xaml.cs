using System;
using System.Collections.Generic;

using Xamarin.Forms;
using CruiseBookingApp.Helpers;

namespace CruiseBookingApp.Views
{
    public partial class DefaultView : ContentPage
    {
        public DefaultView()
        {
            InitializeComponent();

            Title = "Default".ToUpper();

            CustomNavigationPage.SetIsTranslucent(this, false);
        }
    }
}
