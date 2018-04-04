using Android.Content;
using Android.Graphics;
using Android.Widget;
using CruiseBookingApp.Views;
using CruiseBookingApp.Droid.Extensions;
using AView = Android.Views.View;
using Android.Util;
using Xamarin.Forms;
using System.Linq;

[assembly: ExportRenderer(typeof(NavigationPage), typeof(CruiseBookingApp.Droid.Renderers.NavigationPageRenderer))]
namespace CruiseBookingApp.Droid.Renderers
{
    public class NavigationPageRenderer : Xamarin.Forms.Platform.Android.AppCompat.NavigationPageRenderer
    {
        protected Page CurrentPage => Element?.Navigation?.NavigationStack.Last();

        public NavigationPageRenderer(Context context) : base(context) { }

        public override void OnViewAdded(AView child)
        {
            base.OnViewAdded(child);

            if (child is Android.Support.V7.Widget.Toolbar toolbar)
            {
                toolbar.ChildViewAdded += Toolbar_ChildViewAdded;
            }
        }

        public override void OnViewRemoved(AView child)
        {
            base.OnViewRemoved(child);

            if (child is Android.Support.V7.Widget.Toolbar toolbar)
            {
                toolbar.ChildViewAdded -= Toolbar_ChildViewAdded;
            }
        }

        void Toolbar_ChildViewAdded(object sender, ChildViewAddedEventArgs e)
        {
            if (e.Child is TextView textView)
            {
                textView.Typeface = Typeface.CreateFromAsset(Context.Assets, CustomNavigationPage.GetFontFamily(CurrentPage).FontNameToFontFile());
                textView.SetTextSize(ComplexUnitType.Sp, (float)CustomNavigationPage.GetFontSize(CurrentPage));
            }
        }
    }
}

