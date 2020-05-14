using myOApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace myOApp.Services
{
    public interface IEventsService
    {
        Task<IEnumerable<EventViewModel>> GetEvents(DateTime? lastSynchronizationDate = null);

        Task<IEnumerable<EventViewModel>> GetNearbyEvents();

        Task<IEnumerable<EventViewModel>> GetFavoritedEvents();

        Task<EventViewModel> GetEvent(string id);

        Task ToggleFavorite(string id);

        Task ForceRefresh();
    }
}
