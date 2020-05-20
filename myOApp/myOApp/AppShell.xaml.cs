using myOApp.Views;
using Xamarin.Forms;

namespace myOApp
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute("eventdetails", typeof(EventDetailsPage));

            CurrentItem.CurrentItem = profileTab;
        }
    }
}
