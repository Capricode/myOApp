using myOApp.Models;
using myOApp.Themes;
using myOApp.ViewModels;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using Xamarin.Essentials;

namespace myOApp.Services
{
    public class Settings : INotifyPropertyChanged
    {
        private const char Separator = ',';

        static Settings settings;

        public static Settings Current => settings ?? (settings = new Settings());

        public ThemeEnum Theme
        {
            get
            {
                var theme = Preferences.Get(nameof(Theme), ThemeEnum.Default.ToString());
                return (ThemeEnum)Enum.Parse(typeof(ThemeEnum), theme);
            }
            set
            {
                Preferences.Set(nameof(Theme), value.ToString());
                OnPropertyChanged();
            }
        }

        public string Username
        {
            get => Preferences.Get(nameof(Username), string.Empty);
            set
            {
                Preferences.Set(nameof(Username), value);
                OnPropertyChanged();
            }
        }

        static readonly string UserRegionsDefaultValue = string.Empty;
        public ObservableCollection<RegionViewModel> UserRegions
        {
            get
            {
                var regions = Preferences.Get(nameof(UserRegions), UserRegionsDefaultValue);
                if (regions == UserRegionsDefaultValue) return new ObservableCollection<RegionViewModel>();

                return new ObservableCollection<RegionViewModel>(
                    regions.Split(Separator)
                    .Select(x => new RegionViewModel { Region = new Region { Name = x }, Selected = true })
                    .ToList());
            }
            set
            {
                var regions = string.Join(Separator.ToString(), value.Select(x => x.Region.Name));
                Preferences.Set(nameof(UserRegions), regions);
                OnPropertyChanged();
            }
        }

        static readonly string FavoritedEventsDefaultValue = string.Empty;
        public ObservableCollection<string> FavoritedEvents
        {
            get
            {
                var favoritedEventsIds = Preferences.Get(nameof(FavoritedEvents), FavoritedEventsDefaultValue);
                if (favoritedEventsIds == FavoritedEventsDefaultValue) return new ObservableCollection<string>();

                return new ObservableCollection<string>(
                    favoritedEventsIds.Split(Separator)
                    .ToList());
            }
            set
            {
                var favoritedEventsIds = string.Join(Separator.ToString(), value);
                Preferences.Set(nameof(FavoritedEvents), favoritedEventsIds);
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged([CallerMemberName]string name = "") =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
