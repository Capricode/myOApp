using System;
using System.ComponentModel;
using Xamarin.Forms;
using myOApp.ViewModels;
using myOApp.Services;

namespace myOApp.Views
{
    [DesignTimeVisible(false)]
    public partial class BrowsePage : ContentPage
    {
        BrowseViewModel viewModel;

        public BrowsePage()
        {
            InitializeComponent();

            BindingContext = viewModel = new BrowseViewModel();
        }

        async void OnItemSelected(object sender, EventArgs args)
        {
            //var layout = (BindableObject)sender;
            //var item = (EventViewModel)layout.BindingContext;
            //await Navigation.PushAsync(new ItemDetailPage(new EventViewModel(item)));
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            MessagingCenter.Subscribe<EventsService>(this, Constants.Synchronization.NewDataAvailableMessage, async (sender) =>
            {
                viewModel.LoadEventsCommand.Execute(null);
            });

            //bool forceRefresh = (DateTime.UtcNow > (ViewModel?.NextForceRefresh ?? DateTime.UtcNow)) ||
            //    loggedIn != Settings.Current.Email;

            //Load if none, or if 45 minutes has gone by
            //if ((ViewModel?.Sessions?.Count ?? 0) == 0 || forceRefresh)
            //{
            //    ViewModel?.LoadSessionsCommand?.Execute(forceRefresh);
            //}

            // for now we refresh always
            viewModel.LoadEventsCommand.Execute(null);
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            MessagingCenter.Unsubscribe<EventsService>(this, Constants.Synchronization.NewDataAvailableMessage);
        }
    }
}