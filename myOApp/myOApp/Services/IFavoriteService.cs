using myOApp.ViewModels;

namespace myOApp.Services
{
    public interface IFavoriteService
    {
        void ToggleFavorite(ItemViewModel item);
    }
}