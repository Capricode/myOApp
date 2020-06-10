using System;
using System.ComponentModel;
using Xamarin.Forms;
using myOApp.ViewModels;
using myOApp.Services;
using myOApp.Models;
using myOApp.Definitions;

namespace myOApp.Views
{
    [DesignTimeVisible(false)]
    public partial class BrowsePage : ContentPage
    {
        BrowseViewModel vm;

        EventsFilter eventsFilter;

        public BrowsePage()
        {
            InitializeComponent();

            BindingContext = vm = new BrowseViewModel();
        }

        public BrowsePage(string filter)
        {
            var now = DateTime.Today;

            eventsFilter = new EventsFilter();
            if (!Enum.TryParse<Filter>(filter, out Filter filterValue)) return;

            switch (filterValue)
            {
                case Filter.FavoritedEvents:
                    eventsFilter.Predicate = x => x.IsFavorite;
                    eventsFilter.ShouldSortDescending = true;
                    break;
                case Filter.PastEvents:
                    eventsFilter.Predicate = x => x.Date <= now;
                    eventsFilter.ShouldSortDescending = true;
                    break;
                case Filter.UpcomingEvents:
                    eventsFilter.Predicate = x => x.Date >= now;
                    break;
                default:
                    eventsFilter.Predicate = null;
                    break;
            };

            InitializeComponent();

            BindingContext = vm = new BrowseViewModel();
            vm.SelectedFilter = filterValue;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            MessagingCenter.Subscribe<EventsService>(this, Constants.Synchronization.NewDataAvailableMessage, async (sender) =>
            {
                vm.LoadEventsCommand.Execute(eventsFilter);
            });

            MessagingCenter.Subscribe<EventsService, EventViewModel>(this, Constants.Favorites.FavoritesToggledMessage, async (sender, singleEvent) =>
            {
                vm.ToggleFavoriteCommand.Execute(singleEvent);
            });

            //bool forceRefresh = (DateTime.UtcNow > (ViewModel?.NextForceRefresh ?? DateTime.UtcNow)) ||
            //    loggedIn != Settings.Current.Email;

            //Load if none, or if 45 minutes has gone by
            //if ((ViewModel?.Sessions?.Count ?? 0) == 0 || forceRefresh)
            //{
            //    ViewModel?.LoadSessionsCommand?.Execute(forceRefresh);
            //}

            // for now we refresh always
            vm.LoadEventsCommand.Execute(eventsFilter);
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            MessagingCenter.Unsubscribe<EventsService>(this, Constants.Synchronization.NewDataAvailableMessage);
            MessagingCenter.Unsubscribe<EventsService, EventViewModel>(this, Constants.Favorites.FavoritesToggledMessage);
        }
    }
}