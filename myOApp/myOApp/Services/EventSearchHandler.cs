using myOApp.ViewModels;
using System.Linq;
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

        protected override async void OnItemSelected(object singleEvent)
        {
            base.OnItemSelected(singleEvent);

            await (App.Current.MainPage as Xamarin.Forms.Shell).GoToAsync($"itemdetails?id={((EventViewModel)singleEvent).Id}");
        }
    }
}
