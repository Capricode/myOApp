using myOApp.Resources.localization;
using myOApp.Services;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace myOApp.ViewModels
{
    public class EventViewModel : INotifyPropertyChanged
    {
        private readonly IEventsService EventsService = DependencyService.Get<IEventsService>();

        private const string ResultUrl = "https://www.o-l.ch/cgi-bin/results?rl_id=";

        public string Id { get; set; }

        public string Name { get; set; }

        public DateTime Date { get; set; }

        public string Map { get; set; }

        public string Club { get; set; }

        public string Region { get; set; }

        public string EventCenter { get; set; }

        public bool HasEventCenter => !string.IsNullOrEmpty(this.EventCenter);

        public string Link { get; set; }

        public bool HasLink => !string.IsNullOrEmpty(this.Link);

        public int? ResultsId { get; set; }

        public bool HasResults => !string.IsNullOrEmpty(this.ResultsUrl);

        public string ResultsUrl => ResultsId == null ? string.Empty : $"{ResultUrl}{ResultsId}";

        public string ShortDate => Date.ToString("dd.MM.yy");

        public double DateUnix
        {
            get
            {
                var date = new DateTime(this.Date.Year, this.Date.Month, this.Date.Day, 8, 0, 0);
                var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
                return (date.ToUniversalTime() - epoch).TotalSeconds;
            }
        }

        public string Day => Date.ToString("dd");

        public string Month => Date.ToString("MMM");

        public string RegionAndClub => $"{Region} | {Club}";

        private bool isFavorite;
        public bool IsFavorite
        {
            get { return isFavorite; }
            set
            {
                isFavorite = value;
                this.OnPropertyChanged();
            }
        }

        private bool isBusy;
        public bool IsBusy
        {
            get { return isBusy; }
            set
            {
                isBusy = value;
                this.OnPropertyChanged();
            }
        }

        ICommand toggleFavoriteItemCommand;
        public ICommand ToggleFavoriteItemCommand => toggleFavoriteItemCommand ?? (toggleFavoriteItemCommand = new Command(async () => await ExecuteToggleFavoriteItemCommand()));

        private async Task ExecuteToggleFavoriteItemCommand()
        {
            IsBusy = true;

            try
            {
                await this.EventsService.ToggleFavorite(this.Id);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        ICommand goToEventDetailsCommand;
        public ICommand GoToEventDetailsCommand => goToEventDetailsCommand ?? (goToEventDetailsCommand = new Command<EventViewModel>(async (tappedEvent) => await ExecuteGoToEventDetailsCommand(tappedEvent)));

        private async Task ExecuteGoToEventDetailsCommand(EventViewModel tappedEvent)
        {
            if (!isBusy)
            {
                IsBusy = true;

                var state = Shell.Current.CurrentState;
                await Shell.Current.GoToAsync($"{state.Location}/eventdetails?id={((EventViewModel)tappedEvent).Id}");

                IsBusy = false;
            }
        }

        ICommand goToBrowserCommand;
        public ICommand GoToBrowserCommand => goToBrowserCommand ?? (goToBrowserCommand = new Command<string>(async (url) => await ExecuteGoToBrowserCommand(url)));

        private async Task ExecuteGoToBrowserCommand(string url)
        {
            var uri = new Uri(url);
            await Browser.OpenAsync(uri, BrowserLaunchMode.SystemPreferred);
        }

        ICommand goToSbbCommand;
        public ICommand GoToSbbCommand => goToSbbCommand ?? (goToSbbCommand = new Command(async () => await ExecuteGoToSbbCommand()));

        private async Task ExecuteGoToSbbCommand()
        {
            if (await Launcher.CanOpenAsync("sbbmobile://"))
            {
                await Launcher.OpenAsync($"sbbmobile://timetable?to={this.EventCenter}&time={this.DateUnix}&timemode=departure");
            }
            else
            {
                await Browser.OpenAsync($"{AppResources.SbbWebsiteTimetableBaseUrl}?suche=true&nach={this.EventCenter}&datum={this.ShortDate}&zeit=8:00", BrowserLaunchMode.SystemPreferred);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        void OnPropertyChanged([CallerMemberName] string name = "") => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
