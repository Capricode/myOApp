using myOApp.Models;
using myOApp.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace myOApp.ViewModels
{
    public class DashboardViewModel : BaseViewModel
    {
        public IDataStore<Item> DataStore => DependencyService.Get<IDataStore<Item>>();

        //this should be a singleton? now we don't update when there is something
        public ObservableCollection<ItemViewModel> Items { get; } = new ObservableCollection<ItemViewModel>();

        public ObservableCollection<ItemViewModel> FavoritedEvents { get; } = new ObservableCollection<ItemViewModel>();

        public DashboardViewModel()
        {
            //if ((ViewModel?.Sessions?.Count ?? 0) == 0 || forceRefresh)
            {
                LoadItemsCommand.Execute(null);
            }

            foreach (var item in this.Items) //.Where(x => x.IsFavorite)
            {
                FavoritedEvents.Add(item);
            }
        }

        // see SessionsViewModel from app-conference
        ICommand loadItemsCommand;
        public ICommand LoadItemsCommand => loadItemsCommand ?? (loadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand()));

        async Task ExecuteLoadItemsCommand()
        {
            IsBusy = true;

            try
            {
                if (Items.Any()) return;

                var items = await DataStore.GetItemsAsync(true);
                foreach (var item in items)
                {
                    Items.Add(new ItemViewModel
                    {
                        Id = item.Id,
                        Description = item.Description,
                        Text = item.Text,
                        IsFavorite = item.IsFavorite
                    });
                }
                Debug.WriteLine("Weszlem");
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

        ICommand goToSettingsCommand;
        public ICommand GoToSettingsCommand => goToSettingsCommand ?? (goToSettingsCommand = new Command(async () => await Shell.Current.GoToAsync("//accountsettings")));
    }
}
