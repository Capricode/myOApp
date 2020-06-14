using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using myOApp.Resources.localization;
using myOApp.Services;
using myOApp.Themes;
using Xamarin.Essentials;
using Xamarin.Forms;
using Region = myOApp.Models.Region;

namespace myOApp.ViewModels
{
    public class ProfileViewModel : BaseViewModel
    {
        private IEnumerable<Region> Regions = new List<Region> {
            new Region{ Name = "AG", Description = AppResources.RegionAG },
            new Region{ Name = "BE/SO", Description = AppResources.RegionBESO },
            new Region{ Name = "GL/GR", Description = AppResources.RegionGLGR },
            new Region{ Name = "NOS", Description = AppResources.RegionNOS },
            new Region{ Name = "NWS", Description = AppResources.RegionNWS },
            new Region{ Name = "SR", Description = AppResources.RegionSR },
            new Region{ Name = "TI", Description = AppResources.RegionTI },
            new Region{ Name = "ZS", Description = AppResources.RegionZS },
            new Region{ Name = "ZH/SH", Description = AppResources.RegionZHSH },
            new Region{ Name = "AUSL.", Description = AppResources.RegionAusland }
        };

        public ObservableCollection<RegionViewModel> RegionsData { get; } = new ObservableCollection<RegionViewModel>();

        ICommand updateUserRegionsCommand;
        public ICommand UpdateUserRegionCommand => updateUserRegionsCommand ?? (updateUserRegionsCommand = new Command<RegionViewModel>((selectedRegion) => this.UpdateUserRegions(selectedRegion)));

        ICommand goToAboutPageCommand;
        public ICommand GoToAboutPageCommand => goToAboutPageCommand ?? (goToAboutPageCommand = new Command(async () => await Shell.Current.GoToAsync("about")));

        public Settings Settings { get; set; }

        public ProfileViewModel()
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
            get => $"{AppResources.ProfileHello}, {(string.IsNullOrEmpty(this.Name) ? AppResources.ProfileNoName : this.Name )}!";
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