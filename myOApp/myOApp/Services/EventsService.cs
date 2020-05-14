using myOApp.Models;
using myOApp.ViewModels;
using MyOApp.DataAccess;
using MyOApp.DataAccess.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace myOApp.Services
{
    public class EventsService : IEventsService
    {
        private readonly IEventsDatabase EventsDatabase = DependencyService.Get<IEventsDatabase>();

        private readonly IEventMapper EventMapper = DependencyService.Get<IEventMapper>();

        private readonly ISynchronizationCenter SynchronizationCenter = DependencyService.Get<ISynchronizationCenter>();

        public async Task<IEnumerable<EventViewModel>> GetEvents(DateTime? lastSynchronizationDate = null)
        {
            var eventEntities = await this.EventsDatabase.GetAllEvents(lastSynchronizationDate);
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

            MessagingCenter.Send(this, Constants.Favorites.FavoritesToggledMessage, eventAfter);
        }

        public async Task ForceRefresh()
        {
            await this.SynchronizationCenter.RefreshData();

            MessagingCenter.Send(this, Constants.Synchronization.NewDataAvailableMessage);
        }
    }
}
