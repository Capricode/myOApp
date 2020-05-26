using System.Collections.Generic;
using System.Threading.Tasks;

namespace myOApp.DataAccess
{
    public interface ISynchronizationCenter
    {
        Task<bool> RefreshData(IEnumerable<string> favoritedEvents);
    }
}
