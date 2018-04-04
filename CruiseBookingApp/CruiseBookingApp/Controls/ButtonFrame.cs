using System;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Xamarin.Forms;

namespace CruiseBookingApp.Controls
{
    public class ButtonFrame : Frame
    {
        Label buttonLabel = new Label();

        public ButtonFrame()
        {
            BackgroundColor = Color.Transparent;
            HasShadow = false;

            buttonLabel.BackgroundColor = Color.Transparent;
            buttonLabel.VerticalTextAlignment = TextAlignment.Center;
            buttonLabel.HorizontalTextAlignment = TextAlignment.Center;
            buttonLabel.Text = Text;
            buttonLabel.TextColor = TextColor;
            buttonLabel.FontFamily = FontFamily;

            Content = buttonLabel;
        }

        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            switch (propertyName)
            {
                case nameof(Command):
                    UpdateCommand();
                    break;
                case nameof(Content):
                    BackgroundColor = Content.BackgroundColor;
                    break;
                case nameof(FontFamily):
                    buttonLabel.FontFamily = FontFamily;
                    break;
                case nameof(FontSize):
                    buttonLabel.FontSize = FontSize;
                    break;
                case nameof(Text):
                    buttonLabel.Text = Text;
                    break;
                case nameof(TextColor):
                    buttonLabel.TextColor = TextColor;
                    break;
            }
        }

        public static readonly BindableProperty CommandProperty =
            BindableProperty.Create("Command", typeof(ICommand), typeof(ButtonFrame), null);

        public ICommand Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }

        public static readonly BindableProperty TextProperty =
            BindableProperty.Create("Text", typeof(string), typeof(ButtonFrame), string.Empty);

        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        public static readonly BindableProperty TextColorProperty =
            BindableProperty.Create("TextColor", typeof(Color), typeof(ButtonFrame), Color.Default);

        public Color TextColor
        {
            get { return (Color)GetValue(TextColorProperty); }
            set { SetValue(TextColorProperty, value); }
        }

        public static readonly BindableProperty FontFamilyProperty =
            BindableProperty.Create("FontFamily", typeof(string), typeof(ButtonFrame), Font.Default.FontFamily);

        public string FontFamily
        {
            get { return (string)GetValue(FontFamilyProperty); }
            set { SetValue(FontFamilyProperty, value); }
        }

        public static readonly BindableProperty FontSizeProperty =
            BindableProperty.Create("FontSize", typeof(double), typeof(ButtonFrame), Font.Default.FontSize);

        public double FontSize
        {
            get { return (double)GetValue(FontSizeProperty); }
            set { SetValue(FontSizeProperty, value); }
        }

        void UpdateCommand()
        {
            GestureRecognizers.Clear();
            GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = Command
            });
        }
    }
}
