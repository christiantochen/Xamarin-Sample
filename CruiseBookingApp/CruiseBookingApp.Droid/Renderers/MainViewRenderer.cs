using System.Collections.Generic;
using System.Linq;
using Android.Content;
using Android.OS;
using Android.Views;
using BottomNavigationBar;
using BottomNavigationBar.Listeners;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using CruiseBookingApp.Views;

[assembly: ExportRenderer(typeof(MainView), typeof(CruiseBookingApp.Droid.MainViewRenderer))]
namespace CruiseBookingApp.Droid
{
    public class MainViewRenderer : VisualElementRenderer<MainView>, IOnTabClickListener
    {
        BottomBar _bottomBar;
        Page _currentPage;

        int _lastSelectedTabIndex = -1;

        public MainViewRenderer(Context context) : base(context) => AutoPackage = false;

        public void OnTabSelected(int position) => ShowPage(position);

        public void OnTabReSelected(int position) { }

        protected override void OnElementChanged(ElementChangedEventArgs<MainView> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement != null)
            {
                ClearElement(e.OldElement);
            }

            if (e.NewElement != null)
            {
                InitializeElement(e.NewElement);
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                ClearElement(Element);
            }

            base.Dispose(disposing);
        }

        protected override void OnLayout(bool changed, int l, int t, int r, int b)
        {
            if (Element == null)
            {
                return;
            }

            int width = r - l;
            int height = b - t;

            _bottomBar.Measure(
                MeasureSpec.MakeMeasureSpec(width, MeasureSpecMode.Exactly),
                MeasureSpec.MakeMeasureSpec(height, MeasureSpecMode.AtMost));

            // We need to call measure one more time with measured sizes 
            // in order to layout the bottom bar properly
            _bottomBar.Measure(
                MeasureSpec.MakeMeasureSpec(width, MeasureSpecMode.Exactly),
                MeasureSpec.MakeMeasureSpec(_bottomBar.ItemContainer.MeasuredHeight, MeasureSpecMode.Exactly));

            int barHeight = _bottomBar.ItemContainer.MeasuredHeight;

            _bottomBar.Layout(0, height - barHeight, width, b);

            float density = Android.Content.Res.Resources.System.DisplayMetrics.Density;

            double contentWidthConstraint = width / density;
            double contentHeightConstraint = height / density;
            double contentBarHeightConstraint = barHeight / density;

            if (_currentPage != null)
            {
                var renderer = Platform.GetRenderer(_currentPage);

                renderer.Element.Measure(contentWidthConstraint, contentHeightConstraint);
                renderer.Element.Layout(new Rectangle(0, 0, contentWidthConstraint, contentHeightConstraint - contentBarHeightConstraint));
                renderer.UpdateLayout();
            }
        }

        void InitializeElement(MainView element) => PopulateChildren(element);

        void PopulateChildren(MainView element)
        {
            // Unfortunately bottom bar can not be reused so we have to
            // remove it and create the new instance
            _bottomBar?.RemoveFromParent();

            _bottomBar = CreateBottomBar(element.Children);
            AddView(_bottomBar);

            LoadPageContent(0);
        }

        void ClearElement(MainView element)
        {
            if (_currentPage != null)
            {
                IVisualElementRenderer renderer = Platform.GetRenderer(_currentPage);

                if (renderer != null)
                {
                    renderer.View.RemoveFromParent();
                    renderer.View.Dispose();
                    renderer.Dispose();

                    _currentPage = null;
                }

                if (_bottomBar != null)
                {
                    _bottomBar.RemoveFromParent();
                    _bottomBar.Dispose();
                    _bottomBar = null;
                }
            }
        }

        BottomBar CreateBottomBar(IEnumerable<Page> pageIntents)
        {
            var bar = new BottomBar(Context);

            bar.SetOnTabClickListener(this);
            bar.NoTabletGoodness();
            bar.UseFixedMode();
            bar.SetActiveTabColor(Resources.GetColor(Resource.Color.colorAccent, Context.Theme));
            bar.SetTextAppearance(Resource.Style.BottomBarTextStyle);
            bar.SetTypeFace("Fonts/Roboto-Regular.ttf");

            if (Build.VERSION.SdkInt >= BuildVersionCodes.Lollipop)
                bar.OutlineProvider = ViewOutlineProvider.Bounds;

            SetClipToPadding(false);

            PopulateBottomBarItems(bar, pageIntents);

            return bar;
        }

        void PopulateBottomBarItems(BottomBar bar, IEnumerable<Page> pages)
        {
            var barItems = pages.Select((x) => new BottomBarTab(Context.GetIconizeDrawable(x.Icon, iconSize: 24, iconColor: Color.White), x.Title));
            bar.SetItems(barItems.ToArray());
        }

        void LoadPageContent(int position) => ShowPage(position);

        void ShowPage(int position)
        {
            if (position != _lastSelectedTabIndex)
            {
                Element.CurrentPage = Element.Children[position];

                if (Element.CurrentPage != null)
                {
                    LoadPageContent(Element.CurrentPage);
                }
            }

            _lastSelectedTabIndex = position;
        }

        void LoadPageContent(Page page)
        {
            UnloadCurrentPage();

            _currentPage = page;

            LoadCurrentPage();

            Element.CurrentPage = _currentPage;
        }

        void LoadCurrentPage()
        {
            var renderer = Platform.GetRenderer(_currentPage);

            if (renderer == null)
            {
                renderer = Platform.CreateRendererWithContext(_currentPage, Context);
                Platform.SetRenderer(_currentPage, renderer);

                AddView(renderer.View);
            }
            else
            {
                // As we show and hide pages manually OnAppearing and OnDisappearing
                // workflow methods won't be called by the framework. Calling them manually...
                var basePage = _currentPage as Page;
                basePage?.SendAppearing();
            }

            renderer.View.Visibility = ViewStates.Visible;
        }

        void UnloadCurrentPage()
        {
            if (_currentPage != null)
            {
                var basePage = _currentPage as Page;
                basePage?.SendDisappearing();

                var renderer = Platform.GetRenderer(_currentPage);

                if (renderer != null)
                {
                    renderer.View.Visibility = ViewStates.Invisible;
                }
            }
        }
    }
}
