using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace myOApp.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EventLabel : Label
    {
        public EventLabel()
        {
            InitializeComponent();
            label.Text = Label;
            text.Text = Text;
        }

        public static readonly BindableProperty LabelProperty =
          BindableProperty.Create(nameof(Label), typeof(string), typeof(EventLabel), default(string), BindingMode.OneWay);

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

        public new static readonly BindableProperty TextProperty =
          BindableProperty.Create(nameof(Text), typeof(string), typeof(EventLabel), default(string), BindingMode.OneWay);

        public new string Text
        {
            get
            {
                return (string)GetValue(TextProperty);
            }

            set
            {
                SetValue(TextProperty, value);
            }
        }

        protected override void OnPropertyChanged(string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            if (propertyName == LabelProperty.PropertyName)
            {
                label.Text = Label;
            }
            else if (propertyName == TextProperty.PropertyName)
            {
                text.Text = Text;
            }
        }
    }
}