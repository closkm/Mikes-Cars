using MikesCars.Models;

namespace MikesCars.Interfaces
{
    public interface IFavoriteRepository
    {
        void AddToFavTable(int userId, int listingId);
        void DeleteFromFavorite(int userId, int listingId);
        List<int> GetIdsInFav(int userId);
        List<Listing> GetAllItemsInFavorite(int listingId);
        bool CheckIfFav(int userId, int listingId);
    }
}
