using myOApp.Themes;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace myOApp.ViewModels
{
    public class AboutViewModel : BaseViewModel
    {
        public AboutViewModel()
        {
            Title = "About";
            OpenWebCommand = new Command(async () => await Browser.OpenAsync("https://xamarin.com"));
        }

        public ICommand OpenWebCommand { get; }

        ICommand changeThemeCommand;
        public ICommand ChangeThemeCommand => changeThemeCommand ?? (changeThemeCommand = new Command<ThemeEnum>((theme) => this.ChangeTheme(theme)));

        private void ChangeTheme(ThemeEnum theme)
        {
            ICollection<ResourceDictionary> mergedDictionaries = Application.Current.Resources.MergedDictionaries;
            if(mergedDictionaries != null)
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