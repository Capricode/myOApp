using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyOApp.DataAccess.Database
{
    public interface IEventsDatabase
    {
        Task<List<EventEntity>> GetAllEvents(DateTime? lastSynchronizationDate);

        Task<List<EventEntity>> GetNearbyEvents(IEnumerable<string> userRegions);
        
        Task<List<EventEntity>> GetFavoritedEvents();

        Task<EventEntity> GetEvent(string id);

        Task<EventEntity> ToggleFavorite(string id);

        Task<int> SaveEvent(EventEntity singleEvent);

        Task<int[]> SaveEvents(IEnumerable<EventEntity> events);

        Task<int> UpdateEvents(IEnumerable<EventEntity> events);

        Task<int> DeleteAll();
    }
}