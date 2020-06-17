using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace myOApp.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EventButton : Frame
    {
        public EventButton()
        {
            InitializeComponent();

            buttonFrame.IsVisible = IsVisible;
            command.Command = ButtonCommand;
            command.CommandParameter = ButtonCommandParameter;

            label.Text = Label;
            icon.FontFamily = IconFontFamily;
            icon.Text = Icon;
        }

        public static new readonly BindableProperty IsVisibleProperty =
          BindableProperty.Create(nameof(IsVisible), typeof(bool), typeof(EventButton), true);

        public new bool IsVisible
        {
            get { return (bool)GetValue(IsVisibleProperty); }
            set { SetValue(IsVisibleProperty, value); }
        }

        public static readonly BindableProperty ButtonCommandProperty =
          BindableProperty.Create(nameof(ButtonCommand), typeof(ICommand), typeof(EventButton), default(ICommand));

        public ICommand ButtonCommand
        {
            get { return (ICommand)GetValue(ButtonCommandProperty); }
            set { SetValue(ButtonCommandProperty, value); }
        }

        public static readonly BindableProperty ButtonCommandParameterProperty =
          BindableProperty.Create(nameof(ButtonCommandParameter), typeof(object), typeof(EventButton), default(object));

        public object ButtonCommandParameter
        {
            get { return (object)GetValue(ButtonCommandParameterProperty); }
            set { SetValue(ButtonCommandParameterProperty, value); }
        }

        public static readonly BindableProperty LabelProperty =
          BindableProperty.Create(nameof(Label), typeof(string), typeof(EventButton), default(string), BindingMode.OneWay);

        public string Label
        {
            get
            {
                return (string)GetValue(LabelProperty);
            }

            set
            {
                SetValue(LabelProperty, value);
            }
        }

        public static readonly BindableProperty IconProperty =
          BindableProperty.Create(nameof(Icon), typeof(string), typeof(EventButton), default(string), BindingMode.OneWay);

        public string Icon
        {
            get
            {
                return (string)GetValue(IconProperty);
            }

            set
            {
                SetValue(IconProperty, value);
            }
        }

        public static readonly BindableProperty IconFontFamilyProperty =
          BindableProperty.Create(nameof(IconFontFamily), typeof(string), typeof(EventButton), default(string), BindingMode.OneWay);

        public string IconFontFamily
        {
            get
            {
                return (string)GetValue(IconFontFamilyProperty);
            }

            set
            {
                SetValue(IconFontFamilyProperty, value);
            }
        }

        protected override void OnPropertyChanged(string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            if (propertyName == IsVisibleProperty.PropertyName)
            {
                buttonFrame.IsVisible = IsVisible;
            }
            else if (propertyName == ButtonCommandProperty.PropertyName)
            {
                command.Command = ButtonCommand;
            }
            else if (propertyName == ButtonCommandParameterProperty.PropertyName)
            {
                command.CommandParameter = ButtonCommandParameter;
            }
            else if (propertyName == LabelProperty.PropertyName)
            {
                label.Text = Label;
            }
            else if (propertyName == IconProperty.PropertyName)
            {
                icon.Text = Icon;
            }
            else if (propertyName == IconFontFamilyProperty.PropertyName)
            {
                icon.FontFamily = IconFontFamily;
            }
        }
    }
}