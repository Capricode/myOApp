using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using myOApp.Controls;
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

            ICollection<ResourceDictionary> mergedDictionaries = Application.Current.Resources.MergedDictionaries;
            if (mergedDictionaries != null)
            {
                mergedDictionaries.Clear();

                switch (theme)
                {
                    case ThemeEnum.Alternative:
                        mergedDictionaries.Add(new AlternativeTheme());
                        break;
                    case ThemeEnum.Default:
                    default:
                        mergedDictionaries.Add(new DefaultTheme());
                        break;
                }
                Debug.WriteLine($"Changed theme to: {theme.ToString()}");
            }
        }
    }
}