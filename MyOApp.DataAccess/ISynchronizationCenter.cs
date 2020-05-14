using MyOApp.DataAccess.Database;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MyOApp.DataAccess
{
    public interface ISynchronizationCenter
    {
        Task RefreshData();
    }
}
