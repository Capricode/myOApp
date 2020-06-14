using myOApp.DataAccess.Client;
using myOApp.DataAccess.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace myOApp.DataAccess
{
    public class SynchronizationCenter : ISynchronizationCenter
    {
        private readonly IEventsDatabase EventsDatabase = DependencyService.Get<IEventsDatabase>();

        private readonly IEventsClient EventsClient = DependencyService.Get<IEventsClient>();

        public async Task<bool> RefreshData(IEnumerable<string> favoritedEvents)
        {
            // call for new data from EventsClient
            IEnumerable<Event> events;
            try
            {
                events = await this.EventsClient.GetEvents();
            }
            catch (Exception)
            {
                return false;
            }

            // map it to EventEntity
            var eventsEntities = events.Select(x => this.MapToEventEntity(x, favoritedEvents));

            // update in database
            await this.EventsDatabase.SaveEvents(eventsEntities);

            return true;
        }

        private EventEntity MapToEventEntity(Event singleEvent, IEnumerable<string> favoritedEventsIds)
        {
            var id = $"{singleEvent.source}-{singleEvent.idSource}";

            return new EventEntity
            {
                Id = id,
                Name = singleEvent.name,
                Club = singleEvent.organiser,
                Date = DateTimeOffset.FromUnixTimeMilliseconds(singleEvent.date).UtcDateTime.Date,
                Map = singleEvent.map,
                Region = singleEvent.region,
                EventCenter = singleEvent.eventCenter,
                Source = singleEvent.source,
                Link = singleEvent.url,
                ResultsId = singleEvent.resultsId,
                LastModificationDate = DateTimeOffset.FromUnixTimeMilliseconds(singleEvent.lastModification).UtcDateTime.Date,
                IsFavorite = favoritedEventsIds.Any(x => x.Equals(id))
            };
        }
    }
}