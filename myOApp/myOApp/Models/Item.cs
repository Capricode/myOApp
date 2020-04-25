using myOApp.Services;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace myOApp.Models
{
    public class Item
    {
        public string Id { get; set; }

        public string Text { get; set; }

        public string Description { get; set; }

        public bool IsFavorite { get; set; }

    }
}