using myOApp.Services;
using myOApp.ViewModels;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace myOApp.Views
{
    [QueryProperty("EventId", "id")]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EventDetailsPage : ContentPage
    {
        private readonly IEventsService EventsService = DependencyService.Get<IEventsService>();

        EventViewModel vm;

        public EventDetailsPage()
        {
            InitializeComponent();
        }

        public string EventId
        {
            set
            {
                Task.Run(async () =>
                {
                    var eventDetails = await EventsService.GetEvent(value);
                    BindingContext = vm = eventDetails;

                    Title = vm.Name;
                });
            }
        }
    }
}