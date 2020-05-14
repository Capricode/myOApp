using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

using Xamarin.Forms;
using System.Windows.Input;
using myOApp.Services;

namespace myOApp.ViewModels
{
    public class BrowseViewModel : BaseViewModel
    {
        public IEventsService EventsService = DependencyService.Get<IEventsService>();

        public ObservableCollection<EventViewModel> Events { get; } = new ObservableCollection<EventViewModel>();

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
        public ICommand LoadEventsCommand => loadEventsCommand ?? (loadEventsCommand = new Command(async () => await ExecuteLoadEventsCommand()));

        async Task ExecuteLoadEventsCommand()
        {
            try
            {
                IsBusy = true;

                var events = await this.EventsService.GetEvents();
                foreach (var singleEvent in events)
                {
                    Events.Add(singleEvent);
                }

                Debug.WriteLine("Weszlem Browse");
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