using myOApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace myOApp.Services
{
    public class EventSearchHandler : SearchHandler
    {
        protected override async void OnQueryChanged(string oldValue, string newValue)
        {
            base.OnQueryChanged(oldValue, newValue);

            if (string.IsNullOrWhiteSpace(newValue))
            {
                ItemsSource = null;
            }
            else
            {
                var dataStore = DependencyService.Get<IDataStore<Item>>();
                var items = await dataStore.GetItemsAsync();

                // I guess I should change this to ItemViewModel here everywhere, but should work still
                ItemsSource = items
                    .Where(i => i.Text.ToLower().Contains(newValue.ToLower()))
                    .ToList<Item>();
            }
        }

        protected override async void OnItemSelected(object item)
        {
            base.OnItemSelected(item);

            await (App.Current.MainPage as Xamarin.Forms.Shell).GoToAsync($"itemdetails?id={((Item)item).Id}");
        }
    }
}
