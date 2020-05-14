using myOApp.Services;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace myOApp.ViewModels
{
    public class EventViewModel : INotifyPropertyChanged
    {
        private readonly IEventsService EventsService = DependencyService.Get<IEventsService>();

        public string Id { get; set; }

        public string Name { get; set; }

        public DateTime Date { get; set; }

        public string Map { get; set; }

        public string Club { get; set; }

        public string Region { get; set; }

        public string Link { get; set; }

        public string ShortDate => Date.ToString("dd.MM");

        public string Day => Date.ToString("dd");

        public string Month => Date.ToString("MMM");

        public string RegionAndClub => $"{Region} | {Club}";

        private bool isFavorite;
        public bool IsFavorite
        {
            get { return isFavorite; }
            set
            {
                isFavorite = value;
                this.OnPropertyChanged();
            }
        }

        private bool isBusy;
        public bool IsBusy
        {
            get { return isBusy; }
            set
            {
                isBusy = value;
                this.OnPropertyChanged();
            }
        }

        ICommand toggleFavoriteItemCommand;
        public ICommand ToggleFavoriteItemCommand => toggleFavoriteItemCommand ?? (toggleFavoriteItemCommand = new Command(async () => await ExecuteToggleFavoriteItemCommand()));

        private async Task ExecuteToggleFavoriteItemCommand()
        {
            IsBusy = true;

            try
            {
                await this.EventsService.ToggleFavorite(this.Id);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged([CallerMemberName]string name = "") => 
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
