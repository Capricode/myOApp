using myOApp.Definitions;
using myOApp.Services;
using myOApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace myOApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DashboardPage : ContentPage
    {
        DashboardViewModel viewModel;

        public DashboardPage()
        {
            InitializeComponent();

            BindingContext = viewModel = new DashboardViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            MessagingCenter.Subscribe<EventsService>(this, Constants.Synchronization.NewDataAvailableMessage, async (sender) =>
            {
                viewModel.LoadNearbyEventsCommand.Execute(null);
            });

            MessagingCenter.Subscribe<EventsService, EventViewModel>(this, Constants.Favorites.FavoritesToggledMessage, async (sender, singleEvent) =>
            {
                viewModel.ToggleFavoriteCommand.Execute(singleEvent);
            });

            viewModel.LoadNearbyEventsCommand.Execute(null);
            viewModel.LoadFavoriteEventsCommand.Execute(null);

            if (viewModel.NearbyEvents == null || viewModel.NearbyEvents?.Count == 0)
            {
                viewModel.IsBusy = true;

                // something for steering the empty message
                //viewModel.IsEmpty = true;
            }
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            MessagingCenter.Unsubscribe<EventsService>(this, Constants.Synchronization.NewDataAvailableMessage);
            MessagingCenter.Unsubscribe<EventsService, EventViewModel>(this, Constants.Favorites.FavoritesToggledMessage);
        }
    }
}