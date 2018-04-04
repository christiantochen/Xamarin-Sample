using System;
using System.Runtime.CompilerServices;
using Xamarin.Forms;

namespace CruiseBookingApp.Controls
{
    public class StepperLayout : StackLayout
    {
        ExtendedButton _plusButton;
        ExtendedButton _minusButton;
        Label _valueLabel;
        Label _titleLabel;

        public StepperLayout()
        {
            _plusButton = new ExtendedButton { Text = "+" };
            _minusButton = new ExtendedButton { Text = "-" };

            if (Device.RuntimePlatform == Device.Android)
                _plusButton.HeightRequest = _plusButton.WidthRequest =
                    _minusButton.WidthRequest = _minusButton.HeightRequest = 48;
            else if (Device.RuntimePlatform == Device.iOS)
                _plusButton.HeightRequest = _plusButton.WidthRequest =
                    _minusButton.WidthRequest = _minusButton.HeightRequest = 36;

            _plusButton.HorizontalOptions = _minusButton.HorizontalOptions =
                LayoutOptions.End;

            _plusButton.Clicked += Button_Clicked;
            _minusButton.Clicked += Button_Clicked;

            _titleLabel = new Label();
            _titleLabel.HorizontalOptions = LayoutOptions.FillAndExpand;

            _valueLabel = new Label();
            _valueLabel.HorizontalOptions = LayoutOptions.End;

            _titleLabel.VerticalOptions = _valueLabel.VerticalOptions = LayoutOptions.Center;

            Orientation = StackOrientation.Horizontal;

            Children.Add(_titleLabel);
            Children.Add(_minusButton);
            Children.Add(_valueLabel);
            Children.Add(_plusButton);

            UpdateValue();
        }

        void Button_Clicked(object sender, EventArgs e)
        {
            if (sender == _plusButton && Value < Max)
            {
                Value += Increment;
            }
            else if (sender == _minusButton &&
                     Value - Increment >= Min)
            {
                Value -= Increment;
            }
        }

        void UpdateValue()
        {
            if (Value >= Max)
            {
                Value = Max;

                _minusButton.IsEnabled = true;
                _plusButton.IsEnabled = false;
            }
            else if (Value <= Min)
            {
                Value = Min;

                _minusButton.IsEnabled = false;
                _plusButton.IsEnabled = true;
            }
            else
            {
                _minusButton.IsEnabled = true;
                _plusButton.IsEnabled = true;
            }

            _valueLabel.Text = Value.ToString();
            OnValueChanged?.Invoke(this, EventArgs.Empty);
        }

        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            switch (propertyName)
            {
                case nameof(Min):
                case nameof(Value):
                case nameof(Max):
                    UpdateValue();
                    break;
                case nameof(Title):
                    _titleLabel.Text = Title;
                    break;
            }
        }

        public event EventHandler OnValueChanged;

        public static readonly BindableProperty TitleProperty =
            BindableProperty.Create("Title", typeof(string), typeof(StepperLayout), string.Empty);

        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        public static readonly BindableProperty ValueProperty =
            BindableProperty.Create("Value", typeof(double), typeof(StepperLayout), default(double));

        public double Value
        {
            get { return (double)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }

        public static readonly BindableProperty IncrementProperty =
            BindableProperty.Create("Increment", typeof(double), typeof(StepperLayout), 1d);

        public double Increment
        {
            get { return (double)GetValue(IncrementProperty); }
            set { SetValue(IncrementProperty, value); }
        }

        public static readonly BindableProperty MaxProperty =
            BindableProperty.Create("Max", typeof(double), typeof(StepperLayout), 9d);

        public double Max
        {
            get { return (double)GetValue(MaxProperty); }
            set { SetValue(MaxProperty, value); }
        }

        public static readonly BindableProperty MinProperty =
            BindableProperty.Create("Min", typeof(double), typeof(StepperLayout), default(double));

        public double Min
        {
            get { return (double)GetValue(MinProperty); }
            set { SetValue(MinProperty, value); }
        }
    }
}
