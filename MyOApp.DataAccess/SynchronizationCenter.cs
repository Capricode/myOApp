using MyOApp.DataAccess.Client;
using MyOApp.DataAccess.Database;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace MyOApp.DataAccess
{
    public class SynchronizationCenter : ISynchronizationCenter
    {
        private readonly IEventsDatabase EventsDatabase = DependencyService.Get<IEventsDatabase>();

        private readonly IEventsClient EventsClient = DependencyService.Get<IEventsClient>();

        public async Task RefreshData()
        {
            // call for new data from EventsClient
            var events = await this.EventsClient.GetEvents();

            // map it to EventEntity
            var eventsEntities = events.Select(x => this.MapToEventEntity(x));

            // update in database
            await this.EventsDatabase.SaveEvents(eventsEntities);
        }

        private EventEntity MapToEventEntity(Event singleEvent)
        {
            return new EventEntity
            {
                Id = $"{singleEvent.source}-{singleEvent.idSource}",
                Name = singleEvent.name,
                Club = singleEvent.organiser,
                Date = DateTimeOffset.FromUnixTimeMilliseconds(singleEvent.date).UtcDateTime.Date,
                Map = singleEvent.map,
                Region = singleEvent.region,
                Source = singleEvent.source,
                Link = singleEvent.url,
                LastModificationDate = DateTimeOffset.FromUnixTimeMilliseconds(singleEvent.lastModification).UtcDateTime.Date
            };
        }
    }
}