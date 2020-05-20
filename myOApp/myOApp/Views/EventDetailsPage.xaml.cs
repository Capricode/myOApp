using myOApp.Services;
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

                    BindingContext = eventDetails;

                    Title = eventDetails.Name;
                });
            }
        }
    }
}