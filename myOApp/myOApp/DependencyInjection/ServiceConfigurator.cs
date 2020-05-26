using myOApp.Services;
using Xamarin.Forms;

namespace myOApp.DependencyInjection
{
    public static class ServiceConfigurator
    {
        public static void Configure()
        {
            DependencyService.Register<IDialogService, DialogService>();
            DependencyService.Register<IEventsService, EventsService>();
            DependencyService.Register<IEventMapper, EventMapper>();
        }
    }
}
