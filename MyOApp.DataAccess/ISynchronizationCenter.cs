using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyOApp.DataAccess
{
    public interface ISynchronizationCenter
    {
        Task RefreshData(IEnumerable<string> favoritedEvents);
    }
}
