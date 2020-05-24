using myOApp.Models;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace myOApp.ViewModels
{
    public class RegionViewModel : INotifyPropertyChanged
    {

        //doesn't need to be property
        private Region region;
        public Region Region
        {
            get { return region; }
            set
            {
                this.region = value;
                this.OnPropertyChanged();
            }
        }

        private bool selected;
        public bool Selected
        {
            get { return selected; }
            set
            {
                this.selected = value;
                this.OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged([CallerMemberName]string name = "") =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}