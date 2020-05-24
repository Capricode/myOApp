using myOApp.Extensions;
using myOApp.Services;
using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace myOApp.ViewModels
{
    public class DashboardViewModel : BaseViewModel
    {
        public IEventsService EventsService => DependencyService.Get<IEventsService>();

        public ObservableRangeCollection<EventViewModel> NearbyEvents { get; } = new ObservableRangeCollection<EventViewModel>();

        public ObservableRangeCollection<EventViewModel> FavoritedEvents { get; } = new ObservableRangeCollection<EventViewModel>();

        ICommand loadNearbyEventsCommand;
        public ICommand LoadNearbyEventsCommand => loadNearbyEventsCommand ?? (loadNearbyEventsCommand = new Command(async () => await ExecuteLoadNearbyEventsCommand()));

        async Task ExecuteLoadNearbyEventsCommand()
        {
            IsBusy = true;

            try
            {
                var events = await this.EventsService.GetNearbyEvents();
                NearbyEvents.ReplaceRange(events);

                Debug.WriteLine("Weszlem Get Nearby");
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

        ICommand loadFavoriteEventsCommand;
        public ICommand LoadFavoriteEventsCommand => loadFavoriteEventsCommand ?? (loadFavoriteEventsCommand = new Command(async () => await ExecuteLoadFavoriteEventsCommand()));

        async Task ExecuteLoadFavoriteEventsCommand()
        {
            IsBusy = true;

            try
            {
                var events = await this.EventsService.GetFavoritedEvents();
                this.FavoritedEvents.ReplaceRange(events);

                Debug.WriteLine("Weszlem Get Favorited");
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

        ICommand toggleFavoriteCommand;
        public ICommand ToggleFavoriteCommand => toggleFavoriteCommand ?? (toggleFavoriteCommand = new Command<EventViewModel>(async (singleEvent) => await ExecuteToggleFavoriteCommand(singleEvent)));

        async Task ExecuteToggleFavoriteCommand(EventViewModel singleEvent)
        {
            IsBusy = true;

            try
            {

                var favEvent = this.FavoritedEvents.FirstOrDefault(x => x.Id == singleEvent.Id);
                if (favEvent == null)
                {
                    this.FavoritedEvents.Add(singleEvent);
                }
                else
                {
                    this.FavoritedEvents.Remove(favEvent);
                }

                var nearbyEvent = this.NearbyEvents.First(x => x.Id == singleEvent.Id);
                nearbyEvent.IsFavorite = singleEvent.IsFavorite;
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

        ICommand goToSettingsCommand;
        public ICommand GoToSettingsCommand => goToSettingsCommand ?? (goToSettingsCommand = new Command(async () => await Shell.Current.GoToAsync("//profile")));

        ICommand goToFavoritesCommand;
        public ICommand GoToFavoritesCommand => goToFavoritesCommand ?? (goToFavoritesCommand = new Command(async () => await Shell.Current.GoToAsync("//browse/upcoming")));
    }
}