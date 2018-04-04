using System;
using System.Threading.Tasks;
using Xamanimation;
using Xamarin.Forms;

namespace CruiseBookingApp.Controls
{
    public class LoadingLayout : StackLayout
    {
        public event EventHandler OnStarted;
        public event EventHandler OnCompleted;

        ActivityIndicator _loadingIndicator = new ActivityIndicator();

        public LoadingLayout()
        {
            IsVisible = false;

            _loadingIndicator.Color = Color;
            _loadingIndicator.VerticalOptions = LayoutOptions.CenterAndExpand;
            _loadingIndicator.HorizontalOptions = LayoutOptions.CenterAndExpand;

            Children.Add(_loadingIndicator);
        }

        protected override void OnPropertyChanged(string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            if (propertyName == IsRunningProperty.PropertyName)
            {
                ToggleAnimation();
            }
            else if (propertyName == ColorProperty.PropertyName)
            {
                _loadingIndicator.Color = Color;
            }
        }

        public static readonly BindableProperty ColorProperty =
            BindableProperty.Create("Color", typeof(Color), typeof(LoadingLayout), Color.Default);

        public Color Color
        {
            get { return (Color)GetValue(ColorProperty); }
            set { SetValue(ColorProperty, value); }
        }

        public static readonly BindableProperty IsRunningProperty =
            BindableProperty.Create("IsRunning", typeof(bool), typeof(LoadingLayout), false);

        public bool IsRunning
        {
            get { return (bool)GetValue(IsRunningProperty); }
            set { SetValue(IsRunningProperty, value); }
        }

        public static readonly BindableProperty OnStartedAnimationProperty =
            BindableProperty.Create("OnStartedAnimation", typeof(AnimationBase), typeof(LoadingLayout), null);

        public AnimationBase OnStartedAnimation
        {
            get { return (AnimationBase)GetValue(OnStartedAnimationProperty); }
            set { SetValue(OnStartedAnimationProperty, value); }
        }

        public static readonly BindableProperty OnCompletedAnimationProperty =
            BindableProperty.Create("OnCompletedAnimation", typeof(AnimationBase), typeof(LoadingLayout), null);

        public AnimationBase OnCompletedAnimation
        {
            get { return (AnimationBase)GetValue(OnCompletedAnimationProperty); }
            set { SetValue(OnCompletedAnimationProperty, value); }
        }

        async void ToggleAnimation()
        {
            if (IsRunning)
            {
                if (OnStartedAnimation != null)
                    await OnStartedAnimation.Begin();

                ToggleIndicator(IsRunning);

                OnStarted?.Invoke(this, null);
            }
            else
            {
                if (OnCompletedAnimation != null)
                    await OnCompletedAnimation.Begin();

                ToggleIndicator(IsRunning);

                OnCompleted?.Invoke(this, null);
            }

            void ToggleIndicator(bool isRunning)
            {
                IsVisible =
                _loadingIndicator.IsVisible =
                _loadingIndicator.IsRunning =
                    IsRunning;

                Opacity = 1;
            }
        }
    }
}
