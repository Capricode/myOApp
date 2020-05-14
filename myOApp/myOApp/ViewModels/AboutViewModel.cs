using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using myOApp.Services;
using myOApp.Themes;
using Xamarin.Essentials;
using Xamarin.Forms;
using Region = myOApp.Models.Region;

namespace myOApp.ViewModels
{
    public class AboutViewModel : BaseViewModel
    {
        private IEnumerable<Region> Regions = new List<Region> {
            new Region{ Name = "AG", Description = "Aargau" },
            new Region{ Name = "BE/SO", Description = "Bern & Solothurn" },
            new Region{ Name = "GL/GR", Description = "Glarus & Graubünden" },
            new Region{ Name = "NOS", Description = "Nordostschweiz" },
            new Region{ Name = "NWS", Description = "Nordwestschweiz" },
            new Region{ Name = "SR", Description = "Suisse Romand (Westschweiz)" },
            new Region{ Name = "TI", Description = "Ticino" },
            new Region{ Name = "ZS", Description = "Zentralschweiz" },
            new Region{ Name = "ZH/SH", Description = "Zürich & Schaffhausen" },
            new Region{ Name = "AUSL.", Description = "Ausland" }
        };

        public ObservableCollection<RegionViewModel> RegionsData { get; } = new ObservableCollection<RegionViewModel>();

        ICommand updateUserRegionsCommand;
        public ICommand UpdateUserRegionCommand => updateUserRegionsCommand ?? (updateUserRegionsCommand = new Command<RegionViewModel>((selectedRegion) => this.UpdateUserRegions(selectedRegion)));

        public Settings Settings { get; set; }

        public AboutViewModel()
        {
            var anyUserRegions = Settings.Current.UserRegions.Any();
            foreach (var region in Regions)
            {
                RegionsData.Add(new RegionViewModel
                {
                    Region = region,
                    Selected = !anyUserRegions ? false : (Settings.Current.UserRegions.FirstOrDefault(x => x.Region.Name == region.Name) != null ? true : false)
                });
            }
        }

        public string PersonalizedTitle
        {
            get => $"Hello, {(string.IsNullOrEmpty(this.Name) ? "Stranger" : this.Name )}!";
        }

        public string Name
        {
            get => Settings.Current.Username;
            set
            {
                Settings.Current.Username = value;
                this.OnPropertyChanged(nameof(PersonalizedTitle));
            }
        }

        public ThemeEnum Theme
        {
            get
            {
                var theme = Preferences.Get(nameof(Theme), nameof(ThemeEnum.Default));
                return (ThemeEnum)Enum.Parse(typeof(ThemeEnum), theme);
            }
            set
            {
                Preferences.Set(nameof(Theme), value.ToString());
                this.OnPropertyChanged(nameof(ThemeEnum));
            }
        }

        void UpdateUserRegions(RegionViewModel selectedRegion)
        {
            selectedRegion.Selected = !selectedRegion.Selected;

            var userRegions = new List<RegionViewModel>(Settings.Current.UserRegions);

            if (selectedRegion.Selected)
            {
                userRegions.Add(selectedRegion);
            }
            else
            {
                var userRegion = userRegions.First(x => x.Region.Name.Equals(selectedRegion.Region.Name));
                userRegions.Remove(userRegion);
            }

            Settings.Current.UserRegions = new ObservableCollection<RegionViewModel>(userRegions);
        }
    }
}