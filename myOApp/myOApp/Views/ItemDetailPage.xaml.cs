using System;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using myOApp.Models;
using myOApp.ViewModels;
using myOApp.Services;
using System.Threading.Tasks;

namespace myOApp.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    [QueryProperty("ItemId", "id")]
    public partial class ItemDetailPage : ContentPage
    {
        ItemDetailViewModel viewModel;

        public string ItemId
        {
            set
            {
                var dataStore = DependencyService.Get<IDataStore<Item>>();

                Task.Run(async () =>
                {
                    var item = await dataStore.GetItemAsync(value);

                    BindingContext = new ItemDetailViewModel
                    {
                        Item = new ItemViewModel
                        {
                            Id = item.Id,
                            Text = item.Text,
                            Description = item.Description,
                            IsFavorite = item.IsFavorite
                        }
                    };
                });
            }
        }

        public ItemDetailPage(ItemDetailViewModel viewModel)
        {
            InitializeComponent();

            BindingContext = this.viewModel = viewModel;
        }

        public ItemDetailPage()
        {
            InitializeComponent();

            var item = new ItemViewModel
            {
                Text = "Item 1",
                Description = "This is an item description."
            };

            viewModel = new ItemDetailViewModel(item);
            BindingContext = viewModel;
        }
    }
}