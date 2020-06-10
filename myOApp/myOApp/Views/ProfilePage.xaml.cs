using myOApp.Services;
using myOApp.Themes;
using myOApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace myOApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProfilePage : ContentPage
    {
        public ProfilePage()
        {
            InitializeComponent();
            BindingContext = new ProfileViewModel();
        }

        void OnThemeChanged(System.Object sender, System.EventArgs e)
        {
            Picker picker = sender as Picker;
            ThemeEnum theme = (ThemeEnum)picker.SelectedItem;

            Settings.Current.Theme = theme;

            ThemeHelper.ChangeTheme(theme);
        }
    }
}