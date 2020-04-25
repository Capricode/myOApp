using myOApp.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace myOApp.ViewModels
{
    public class ItemViewModel : BaseViewModel
    {
		private IFavoriteService FavoriteService => DependencyService.Resolve<IFavoriteService>();

		private string id;
		public string Id
		{
			get { return id; }
			set { SetProperty(ref id, value); }
		}

		private string text;
		public string Text
		{
			get { return text; }
			set { SetProperty(ref text, value); }
		}

		private string description;
		public string Description
		{
			get { return description; }
			set { SetProperty(ref description, value); }
		}

		private bool isFavorite;
		public bool IsFavorite
		{
			get { return isFavorite; }
			set { SetProperty(ref isFavorite, value); }
		}

		ICommand toggleFavoriteItemCommand;
		public ICommand ToggleFavoriteItemCommand => toggleFavoriteItemCommand ?? (toggleFavoriteItemCommand = new Command(async () => await ExecuteToggleFavoriteItemCommand()));

		private async Task ExecuteToggleFavoriteItemCommand()
		{
			IsBusy = true;

			try
			{
				this.FavoriteService.ToggleFavorite(this);
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
	}
}
