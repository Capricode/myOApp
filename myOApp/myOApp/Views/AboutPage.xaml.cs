using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using myOApp.Controls;
using myOApp.Services;
using myOApp.Themes;
using myOApp.ViewModels;
using Xamarin.Forms;

namespace myOApp.Views
{
    [DesignTimeVisible(false)]
    public partial class AboutPage : ContentPage
    {
        public AboutPage()
        {
            InitializeComponent();
            BindingContext = new AboutViewModel();
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