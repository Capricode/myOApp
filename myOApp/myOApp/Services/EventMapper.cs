using myOApp.ViewModels;
using MyOApp.DataAccess.Database;
using System;
using System.Collections.Generic;
using System.Linq;

namespace myOApp.Services
{
    public class EventMapper : IEventMapper
    {
        public EventEntity MapToDatabase(EventViewModel viewModel)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<EventViewModel> MapToViewModel(List<EventEntity> eventEntities)
        {
            if (eventEntities == null || !eventEntities.Any()) return new List<EventViewModel>();

            return eventEntities.Select(eventEntity => this.MapToViewModel(eventEntity)).Where(singleEvent => singleEvent != null);
        }

        public EventViewModel MapToViewModel(EventEntity eventEntity)
        {
            if (eventEntity == null) return null;

            return new EventViewModel
            {
                Id = eventEntity.Id,
                Name = eventEntity.Name,
                Club = eventEntity.Club,
                Map = eventEntity.Map,
                Date = eventEntity.Date,
                Region = eventEntity.Region,
                Link = eventEntity.Link,
                IsFavorite = eventEntity.IsFavorite
            };
        }
    }
}
