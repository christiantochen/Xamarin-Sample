using System;
using Android.Content;
using Android.Widget;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using CruiseBookingApp.Controls;
using CruiseBookingApp.Droid.Renderers;
using CruiseBookingApp.ViewModels.Base;
using CruiseBookingApp.Views;

[assembly: ExportRenderer(typeof(ExtendedListView), typeof(ExtendedListViewRenderer))]
namespace CruiseBookingApp.Droid.Renderers
{
    public class ExtendedListViewRenderer : ListViewRenderer
    {
        public ExtendedListViewRenderer(Context context) : base(context) { }

        public ExtendedListView ExtendedListView => Element as ExtendedListView;

        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.ListView> e)
        {
            base.OnElementChanged(e);

            if (e.NewElement != null)
            {
                Control.FastScrollEnabled = true;
                Control.HorizontalScrollBarEnabled = false;
                Control.ScrollingCacheEnabled = false;
                Control.ScrollbarFadingEnabled = false;
                Control.SmoothScrollbarEnabled = true;
                Control.VerticalScrollBarEnabled = false;

                if (ExtendedListView.BindingContext is IHandleExtendedListView extendedListViewContext)
                    extendedListViewContext.EnableExtendedListViewScroll = SetControlScroll;
            }
        }

        void SetControlScroll(bool enable)
        {
            if (enable)
            {
                Control.Scroll -= Control_Scroll;
                Control.Scroll += Control_Scroll;
            }
            else
            {
                lastY = -1;
                Control.Scroll -= Control_Scroll;
            }
        }

        int lastY = -1;
        void Control_Scroll(object sender, AbsListView.ScrollEventArgs e)
        {
            int y = (int)(-e.View.GetChildAt(0).GetY() / 2);

            if (y == lastY)
                return;

            if (e.FirstVisibleItem == 0 && y <= 255 && y >= 0)
            {
                CustomNavigationPage.SetBarBackgroundColor(Color.FromRgba(255, 80, 80, lastY = y));
            }
            else if (e.FirstVisibleItem != 0 && Math.Abs(lastY - 1) > double.Epsilon)
            {
                CustomNavigationPage.SetBarBackgroundColor(Color.FromRgba(255, 80, 80, lastY = 255));
            }
        }
    }
}
