using myOApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace myOApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AboutPage : ContentPage
    {
        AboutViewModel vm;

        public AboutPage()
        {
            InitializeComponent();

            BindingContext = vm = new AboutViewModel();
        }
    }
}