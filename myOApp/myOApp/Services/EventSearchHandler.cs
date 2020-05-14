using myOApp.Models;
using myOApp.ViewModels;
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
                var eventsService = DependencyService.Get<IEventsService>();
                var events = await eventsService.GetEvents();

                ItemsSource = events
                    .Where(x => x.Name.ToLower().Contains(newValue.ToLower()))
                    .ToList<EventViewModel>();
            }
        }

        protected override async void OnItemSelected(object item)
        {
            base.OnItemSelected(item);

            await (App.Current.MainPage as Xamarin.Forms.Shell).GoToAsync($"itemdetails?id={((Item)item).Id}");
        }
    }
}
