using System;
using System.Collections.Generic;
using FFImageLoading.Forms;
using Xamarin.Forms;
using CruiseBookingApp.Models;

namespace CruiseBookingApp.Views.Templates
{
    public partial class FirstViewCategoryItemsTemplate : ViewCell
    {
        public FirstViewCategoryItemsTemplate()
        {
            InitializeComponent();
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();

            if (BindingContext is FirstViewCategory category)
            {
                foreach (var item in category.Items)
                {
                    CategoryItemGrid.Children.Add(new CachedImage
                    {
                        HeightRequest = 150,
                        DownsampleHeight = 150,
                        Aspect = Aspect.Fill,
                        Source = item
                    }, CategoryItemGrid.Children.Count, 0);
                }
            }
        }
    }
}
