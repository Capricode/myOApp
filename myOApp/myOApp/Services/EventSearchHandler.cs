using myOApp.ViewModels;
using System.Linq;
using System.Threading.Tasks;
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
                    .Where(x => x.Name.ToLower()
                    .Contains(newValue.ToLower()))
                    .ToList<EventViewModel>();
            }
        }

        protected override async void OnItemSelected(object singleEvent)
        {
            base.OnItemSelected(singleEvent);

            // this line makes the search show the event details page
            // https://github.com/xamarin/Xamarin.Forms/issues/5713
            await Task.Delay(400);

            var state = Shell.Current.CurrentState;
            await Shell.Current.GoToAsync($"{state.Location}/eventdetails?id={((EventViewModel)singleEvent).Id}");
        }
    }
}
