using myOApp.Models;
using myOApp.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace myOApp.Services
{
    public interface IEventsService
    {
        Task<IEnumerable<EventViewModel>> GetEvents(EventsFilter eventsFilter = null);

        Task<IEnumerable<EventViewModel>> GetNearbyEvents();

        Task<IEnumerable<EventViewModel>> GetFavoritedEvents();

        Task<EventViewModel> GetEvent(string id);

        Task<bool> ToggleFavorite(string id);

        Task ForceRefresh();
    }
}
