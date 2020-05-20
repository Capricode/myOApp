using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MyOApp.DataAccess.Database
{
    public interface IEventsDatabase
    {
        Task<List<EventEntity>> GetAllEvents(Expression<Func<EventEntity, bool>> predicate = null, bool shouldSortDescending = false, DateTime? lastSynchronizationDate = null);

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