using System;
using System.Diagnostics;
using System.Threading.Tasks;

using Xamarin.Forms;
using System.Windows.Input;
using myOApp.Services;
using myOApp.Extensions;
using System.Linq;
using myOApp.Models;

namespace myOApp.ViewModels
{
    public class BrowseViewModel : BaseViewModel
    {
        public IEventsService EventsService = DependencyService.Get<IEventsService>();

        public Filter SelectedFilter;

        public bool IsFavoritedFilterSelected => SelectedFilter == Filter.FavoritedEvents;

        public ObservableRangeCollection<EventViewModel> Events { get; } = new ObservableRangeCollection<EventViewModel>();

        private bool isRefresh;
        public bool IsRefresh
        {
            get { return isRefresh; }
            set
            {
                isRefresh = value;
                this.OnPropertyChanged();
            }
        }

        public BrowseViewModel()
        {
            Title = "Browse";

            //if ((ViewModel?.Sessions?.Count ?? 0) == 0 || forceRefresh)
            //{
            //    LoadItemsCommand.Execute(null);
            //}

            //foreach (var item in this.Items)//base.Items.Where(x => x.IsFavorite))
            //{
            //    FavoritedEvents.Add(item);
            //}
        }

        // see SessionsViewModel from app-conference
        ICommand loadEventsCommand;
        public ICommand LoadEventsCommand => loadEventsCommand ?? (loadEventsCommand = new Command<EventsFilter>(async (eventsFilter) => await ExecuteLoadEventsCommand(eventsFilter)));

        async Task ExecuteLoadEventsCommand(EventsFilter eventsFilter)
        {
            try
            {
                IsBusy = true;

                var events = await this.EventsService.GetEvents(eventsFilter);
                this.Events.ReplaceRange(events);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                // if there were no local events in db yet, we still wait for data sync
                if (Events == null || Events?.Count == 0)
                {
                    IsBusy = true;
                }
                else
                {
                    IsBusy = false;
                }
            }
        }

        ICommand toggleFavoriteCommand;
        public ICommand ToggleFavoriteCommand => toggleFavoriteCommand ?? (toggleFavoriteCommand = new Command<EventViewModel>(async (singleEvent) => await ExecuteToggleFavoriteCommand(singleEvent)));

        async Task ExecuteToggleFavoriteCommand(EventViewModel singleEvent)
        {
            IsBusy = true;

            try
            {
                var toggledEvent = this.Events.First<EventViewModel>(x => x.Id == singleEvent.Id);
                toggledEvent.IsFavorite = singleEvent.IsFavorite;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        ICommand forceRefreshEventsCommand;
        public ICommand ForceRefreshEventsCommand => forceRefreshEventsCommand ?? (forceRefreshEventsCommand = new Command(async () => await ExecuteRefreshEventsCommand()));

        async Task ExecuteRefreshEventsCommand()
        {
            try
            {
                // IsRefresh = true;
                IsBusy = true;
                await this.EventsService.ForceRefresh();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
                IsRefresh = false;
            }
        }
    }
}