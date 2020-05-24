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

        public string Link { get; set; }

        public bool HasLink => !string.IsNullOrEmpty(this.Link);

        public int? ResultsId { get; set; }

        public bool HasResults => !string.IsNullOrEmpty(this.ResultsUrl);

        public string ResultsUrl => ResultsId == null ? string.Empty : $"{ResultUrl}{ResultsId}";

        public string ShortDate => Date.ToString("dd.MM.yy");

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
            var state = Shell.Current.CurrentState;
            await Shell.Current.GoToAsync($"{state.Location}/eventdetails?id={((EventViewModel)tappedEvent).Id}");
            //await (App.Current.MainPage as Xamarin.Forms.Shell).GoToAsync($"eventdetails?id={((EventViewModel)tappedEvent).Id}");
        }

        ICommand goToBrowserCommand;
        public ICommand GoToBrowserCommand => goToBrowserCommand ?? (goToBrowserCommand = new Command<string>(async (url) => await ExecuteGoToBrowserCommand(url)));

        private async Task ExecuteGoToBrowserCommand(string url)
        {
            var uri = new Uri(url);
            await Browser.OpenAsync(uri, BrowserLaunchMode.SystemPreferred);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged([CallerMemberName]string name = "") => 
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
