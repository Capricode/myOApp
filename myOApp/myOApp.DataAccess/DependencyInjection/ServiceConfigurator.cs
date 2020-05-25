using myOApp.DataAccess.Client;
using myOApp.DataAccess.Database;
using Xamarin.Forms;

namespace myOApp.DataAccess.DependencyInjection
{
    public static class ServiceConfigurator
    {
        public static void Configure()
        {
            DependencyService.Register<IEventsClient, EventsClient>();
            DependencyService.Register<IEventsDatabase, EventsDatabase>();
            DependencyService.Register<ISynchronizationCenter, SynchronizationCenter>();
        }
    }
}
