using MyOApp.DataAccess.Client;
using MyOApp.DataAccess.Database;
using Xamarin.Forms;

namespace MyOApp.DataAccess.DependencyInjection
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
