using myOApp.ViewModels;

namespace myOApp.Services
{
    public class FavoriteService : IFavoriteService
    {
        public void ToggleFavorite(ItemViewModel item)
        {
            if (item.IsFavorite)
            {
                item.IsFavorite = false;
            }
            else 
            {
                item.IsFavorite = true;
            }
        }
    }
}
