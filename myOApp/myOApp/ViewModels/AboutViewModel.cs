using System.Collections.Generic;
using System.Linq;
using myOApp.Controls;
using myOApp.Extensions;
using myOApp.Models;

namespace myOApp.ViewModels
{
    public class AboutViewModel : BaseViewModel
    {
        private IEnumerable<Region> Regions = new List<Region> {
            new Region{Name="AG", Description=""},
            new Region{Name="BE/SO", Description=""},
            new Region{Name="GL/GR", Description=""},
            new Region{Name="NOS", Description=""},
            new Region{Name="NWS", Description=""},
            new Region{Name="SR", Description=""},
            new Region{Name="TI", Description=""},
            new Region{Name="ZH/SH", Description=""},
            new Region{Name="ZS", Description=""},
            new Region{Name="AUSL.", Description=""}
        };

        public List<SelectableData<Region>> RegionsData { get; set; }

        public AboutViewModel()
        {
            RegionsData = Regions.Select(x => new SelectableData<Region> { Data = x, Selected = false }).ToList();
        }

        private string personalizedTitle;
        public string PersonalizedTitle
        {
            get { return personalizedTitle; }
            set { SetProperty(ref name, $"Hello { (!string.IsNullOrWhiteSpace(this.Name) ? this.Name : "Stranger!")}"); }
        }

        private string name;
        public string Name
        {
            get { return name; }
            set { SetProperty(ref name, value); SetProperty(ref personalizedTitle, value); }
        }

        private ThemeEnum theme;
        public ThemeEnum Theme
        {
            get { return theme; }
            set { SetProperty(ref theme, value); }
        }
    }
}