using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Xamarin.Forms;

namespace myOApp.Themes
{
    public static class ThemeHelper
    {
        public static ThemeEnum CurrentTheme;

        public static void ChangeTheme(ThemeEnum theme)
        {
            if (CurrentTheme == theme) return;

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

                CurrentTheme = theme;

                Debug.WriteLine($"Changed theme to: {theme.ToString()}");
            }
        }
    }
}
