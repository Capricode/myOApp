using myOApp.ViewModels;
using myOApp.DataAccess.Database;
using System.Collections.Generic;

namespace myOApp.Services
{
    public interface IEventMapper
    {
        EventEntity MapToDatabase(EventViewModel viewModel);

        EventViewModel MapToViewModel(EventEntity eventEntity);

        IEnumerable<EventViewModel> MapToViewModel(List<EventEntity> eventEntities);
    }
}
