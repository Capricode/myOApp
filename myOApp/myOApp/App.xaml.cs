using Xamarin.Forms;
using myOApp.Services;
using myOApp.DependencyInjection;
using MyOApp.DataAccess;
using myOApp.Themes;

[assembly: ExportFont("fa-regular-400.ttf", Alias = "FA-R")]
[assembly: ExportFont("fa-solid-900.ttf", Alias = "FA-S")]
[assembly: ExportFont("fa-brands-400.ttf", Alias = "FA-B")]

[assembly: ExportFont("RobotoCondensed-Bold.ttf", Alias = "Roboto-B")]
[assembly: ExportFont("RobotoCondensed-BoldItalic.ttf", Alias = "Roboto-BI")]
[assembly: ExportFont("RobotoCondensed-Regular.ttf", Alias = "Roboto")]
[assembly: ExportFont("RobotoCondensed-Italic.ttf", Alias = "Roboto-I")]
[assembly: ExportFont("RobotoCondensed-Light.ttf", Alias = "Roboto-L")]
[assembly: ExportFont("RobotoCondensed-LightItalic.ttf", Alias = "Roboto-LI")]
namespace myOApp
{
    public partial class App : Application
    {
        IEventsService EventsService { get; set; }

        public App()
        {
            InitializeComponent();


            ServiceConfigurator.Configure();
            MyOApp.DataAccess.DependencyInjection.ServiceConfigurator.Configure();

            EventsService = DependencyService.Get<IEventsService>();

            MainPage = new AppShell();

            ThemeHelper.ChangeTheme(Settings.Current.Theme);
        }

        protected override async void OnStart()
        {
            await EventsService.ForceRefresh();
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
