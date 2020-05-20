using myOApp.Models;
using myOApp.ViewModels;
using MyOApp.DataAccess;
using MyOApp.DataAccess.Database;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace myOApp.Services
{
    public class EventsService : IEventsService
    {
        private readonly IEventsDatabase EventsDatabase = DependencyService.Get<IEventsDatabase>();

        private readonly IEventMapper EventMapper = DependencyService.Get<IEventMapper>();

        private readonly ISynchronizationCenter SynchronizationCenter = DependencyService.Get<ISynchronizationCenter>();

        public async Task<IEnumerable<EventViewModel>> GetEvents(EventsFilter eventsFilter = null)
        {
            if (eventsFilter == null) eventsFilter = new EventsFilter();

            var eventEntities = await this.EventsDatabase.GetAllEvents(eventsFilter.Predicate, eventsFilter.ShouldSortDescending);
            return this.EventMapper.MapToViewModel(eventEntities);
        }

        public async Task<IEnumerable<EventViewModel>> GetNearbyEvents()
        {
            var userRegions = Settings.Current.UserRegions.Select(x => x.Region.Name);
            var eventEntities = await this.EventsDatabase.GetNearbyEvents(userRegions);
            return this.EventMapper.MapToViewModel(eventEntities);
        }

        public async Task<IEnumerable<EventViewModel>> GetFavoritedEvents()
        {
            var eventEntities = (await this.EventsDatabase.GetFavoritedEvents());
            return this.EventMapper.MapToViewModel(eventEntities);
        }

        public async Task<EventViewModel> GetEvent(string id)
        {
            var singleEvent = await this.EventsDatabase.GetEvent(id);
            return this.EventMapper.MapToViewModel(singleEvent);
        }

        public async Task ToggleFavorite(string id)
        {
            var eventDbAfter = await this.EventsDatabase.ToggleFavorite(id);

            var eventAfter = this.EventMapper.MapToViewModel(eventDbAfter);

            this.ToggleFavoritedEventsSettings(eventAfter);

            MessagingCenter.Send(this, Constants.Favorites.FavoritesToggledMessage, eventAfter);
        }

        public async Task ForceRefresh()
        {
            var favoritedEvents = Settings.Current.FavoritedEvents.ToList();
            await this.SynchronizationCenter.RefreshData(favoritedEvents);

            MessagingCenter.Send(this, Constants.Synchronization.NewDataAvailableMessage);
        }

        private void ToggleFavoritedEventsSettings(EventViewModel eventAfter)
        {
            var favoritedEvents = new List<string>(Settings.Current.FavoritedEvents);

            if (eventAfter.IsFavorite)
            {
                favoritedEvents.Add(eventAfter.Id);
            }
            else
            {
                favoritedEvents.Remove(eventAfter.Id);
            }

            Settings.Current.FavoritedEvents = new ObservableCollection<string>(favoritedEvents);
        }
    }
}
