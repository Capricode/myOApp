using myOApp.Views;
using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace myOApp
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute("itemdetails", typeof(ItemDetailPage));
            //Routing.RegisterRoute("accountsettings", typeof(AboutPage));
        }
    }
}
