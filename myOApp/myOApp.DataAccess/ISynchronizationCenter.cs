using System.Collections.Generic;
using System.Threading.Tasks;

namespace myOApp.DataAccess
{
    public interface ISynchronizationCenter
    {
        Task RefreshData(IEnumerable<string> favoritedEvents);
    }
}
