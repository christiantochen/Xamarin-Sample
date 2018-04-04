using System;
using System.Collections.Generic;
using FFImageLoading.Transformations;
using Xamarin.Forms;

namespace CruiseBookingApp.Views.Templates
{
    public partial class FirstViewBannerItemTemplate : ContentView
    {
        public FirstViewBannerItemTemplate()
        {
            InitializeComponent();

            cachedImage.Transformations.Add(new ColorSpaceTransformation
            {
                RGBAWMatrix = FFColorMatrix.ColorToTintMatrix(64, 64, 64, 128)
            });
        }
    }
}