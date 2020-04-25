using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using myOApp.Services;
using myOApp.Views;

namespace myOApp
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            DependencyService.Register<MockDataStore>();
            DependencyService.Register<IFavoriteService, FavoriteService>();

            MainPage = new AppShell();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
