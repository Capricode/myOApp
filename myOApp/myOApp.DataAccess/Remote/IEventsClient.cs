using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace myOApp.DataAccess.Client
{
    public interface IEventsClient
    {
        Task<IEnumerable<Event>> GetEvents(DateTime? lastModificationDate = null, int? year = null);
    }
}
