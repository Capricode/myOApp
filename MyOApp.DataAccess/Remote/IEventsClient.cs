using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyOApp.DataAccess.Client
{
    public interface IEventsClient
    {
        Task<IEnumerable<Event>> GetEvents(DateTime? lastModificationDate = null, int? year = null);
    }
}
