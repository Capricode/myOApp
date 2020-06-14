using Xamarin.Forms;
using myOApp.Services;
using myOApp.DependencyInjection;
using myOApp.Themes;
using myOApp.Definitions;
using myOApp.Resources.localization;

[assembly: ExportFont("fa-regular-400.ttf", Alias = "FA-R")]
[assembly: ExportFont("fa-solid-900.ttf", Alias = "FA-S")]
[assembly: ExportFont("RobotoCondensed-Regular.ttf", Alias = "RobotoCondensed")]
[assembly: ExportFont("Roboto-Light.ttf", Alias = "Roboto-L")]
namespace myOApp
{
    public partial class App : Application
    {
        IEventsService EventsService { get; set; }

        IDialogService DialogService { get; set; }

        public App()
        {
            InitializeComponent();

            ServiceConfigurator.Configure();
            myOApp.DataAccess.DependencyInjection.ServiceConfigurator.Configure();

            EventsService = DependencyService.Get<IEventsService>();
            DialogService = DependencyService.Get<IDialogService>();

            MainPage = new AppShell();

            ThemeHelper.ChangeTheme(Settings.Current.Theme);
        }

        protected override async void OnStart()
        {
            this.OnResume();

            await EventsService.ForceRefresh();
        }

        protected override void OnResume()
        {
            MessagingCenter.Subscribe<EventsService>(this, Constants.Synchronization.NoConnectionMessage, async (sender) =>
            {
                await this.DialogService.ShowMessage($"{AppResources.AppCouldNotRefreshDataNoInternetConnection}", AppResources.DialogAlertTitle);
            });
        }

        protected override void OnSleep()
        {
            MessagingCenter.Unsubscribe<EventsService>(this, Constants.Synchronization.NoConnectionMessage);
        }
    }
}
