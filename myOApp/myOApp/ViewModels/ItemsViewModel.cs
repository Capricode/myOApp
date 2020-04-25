using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

using Xamarin.Forms;

using myOApp.Models;
using myOApp.Views;
using System.Windows.Input;
using System.Linq;
using myOApp.Services;

namespace myOApp.ViewModels
{
    public class ItemsViewModel : BaseViewModel
    {
        public IDataStore<Item> DataStore => DependencyService.Get<IDataStore<Item>>();

        //this should be a singleton? now we don't update when there is something
        public ObservableCollection<ItemViewModel> Items { get; } = new ObservableCollection<ItemViewModel>();

        public ObservableCollection<ItemViewModel> FavoritedEvents { get; } = new ObservableCollection<ItemViewModel>();


        public ItemsViewModel()
        {
            Title = "Browse";

            //if ((ViewModel?.Sessions?.Count ?? 0) == 0 || forceRefresh)
            {
                LoadItemsCommand.Execute(null);
            }

            foreach (var item in this.Items)//base.Items.Where(x => x.IsFavorite))
            {
                FavoritedEvents.Add(item);
            }

            MessagingCenter.Subscribe<NewItemPage, ItemViewModel>(this, "AddItem", async (obj, item) =>
            {
                Items.Add(item as ItemViewModel);

                var newItem = new Item
                {
                    Text = item.Text,
                    Description = item.Description
                };

                await DataStore.AddItemAsync(newItem);
            });
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
    }
}