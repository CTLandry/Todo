using System;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Xamarin.Forms;

namespace Todo.Controls
{
    public class CheckBox : ContentView
    {
        #region Member Variable

        private Image _Image;
        private ICommand _toggleCommand;

        #endregion

        #region Constructor

        public CheckBox()
        {
            Initialize();
            HorizontalOptions = VerticalOptions = new LayoutOptions(LayoutAlignment.Center, false);
            Content = _Image;

            GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = _toggleCommand
            });
        }

        #endregion

        #region Private Methods 

        private void Initialize()
        {
            _Image = new Image
            {
                HeightRequest = ImageHeightRequest,
                WidthRequest = ImageWidthRequest,
                Source = IsChecked ? CheckedImage : UnCheckedImage
            };

            _toggleCommand = new Command(OnTappedCommand);
        }

        private void OnTappedCommand(object obj)
        {
            IsChecked = !IsChecked;
        }

        #endregion

        #region Events

        public event EventHandler<TappedEventArgs> IsCheckedChanged;

        #endregion

        #region Bindable Properties

        public static BindableProperty CheckedImageProperty = BindableProperty.Create(nameof(CheckedImage), typeof(string), typeof(CheckBox), "");

        public static BindableProperty UnCheckedImageProperty = BindableProperty.Create(nameof(UnCheckedImage), typeof(string), typeof(CheckBox), "");

        public static BindableProperty ImageHeightRequestProperty = BindableProperty.Create(nameof(ImageHeightRequest), typeof(int), typeof(CheckBox), 24);

        public static BindableProperty ImageWidthRequestProperty = BindableProperty.Create(nameof(ImageWidthRequest), typeof(int), typeof(CheckBox), 24);

        public static BindableProperty IsCheckedProperty = BindableProperty.Create(nameof(IsChecked), typeof(bool), typeof(CheckBox), false, BindingMode.TwoWay, propertyChanged: OnCheckedChanged);

        public static readonly BindableProperty CheckedCommandProperty = BindableProperty.Create(
            propertyName: nameof(CheckedCommand),
            returnType: typeof(ICommand),
            declaringType: typeof(CheckBox),
            defaultValue: null, BindingMode.TwoWay);

        public static readonly BindableProperty CommandParameterProperty = BindableProperty.Create(
            propertyName: nameof(CommandParameter),
            returnType: typeof(object),
            declaringType: typeof(CheckBox),
            defaultValue: null);


        #endregion

        #region public Properties

        public string CheckedImage
        {
            get { return (string)GetValue(CheckedImageProperty); }
            set { SetValue(CheckedImageProperty, value); }
        }

        public object CommandParameter
        {
            get { return GetValue(CommandParameterProperty); }
            set { SetValue(CommandParameterProperty, value); }
        }

        public string UnCheckedImage
        {
            get { return (string)GetValue(UnCheckedImageProperty); }
            set { SetValue(UnCheckedImageProperty, value); }
        }

        public int ImageWidthRequest
        {
            get { return (int)GetValue(ImageWidthRequestProperty); }
            set { SetValue(ImageWidthRequestProperty, value); }
        }

        public int ImageHeightRequest
        {
            get { return (int)GetValue(ImageHeightRequestProperty); }
            set { SetValue(ImageHeightRequestProperty, value); }
        }

        public bool IsChecked
        {
            get { return (bool)GetValue(IsCheckedProperty); }
            set { SetValue(IsCheckedProperty, value); }
        }

        public ICommand CheckedCommand
        {
            get { return (ICommand)GetValue(CheckedCommandProperty); }
            set { SetValue(CheckedCommandProperty, value); }
        }


        #endregion

        #region OnPropertyChanged

        private void CallBackMethod()
        {

        }

        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            if (propertyName == nameof(IsChecked))
            {
                _Image.Source = IsChecked ? CheckedImage : UnCheckedImage;
                return;
            }

            if (propertyName == nameof(ImageHeightRequest))
            {
                _Image.WidthRequest = ImageHeightRequest;
                return;
            }

            if (propertyName == nameof(ImageWidthRequest))
            {
                _Image.HeightRequest = ImageWidthRequest;
                return;
            }

            if (propertyName == nameof(CheckedImage) || propertyName == nameof(UnCheckedImage))
            {
                _Image.Source = IsChecked ? CheckedImage : UnCheckedImage;
                return;
            }
        }
        private static void OnCheckedChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (oldValue != newValue)
            {
                (bindable as CheckBox).IsCheckedChanged?.Invoke(bindable, new TappedEventArgs(newValue));
                (bindable as CheckBox).CheckedCommand?.Execute((bindable as CheckBox).CommandParameter);
            }
        }

        #endregion
    }
}
