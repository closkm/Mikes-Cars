using MikesCars.Models;

namespace MikesCars.Interfaces
{
    public interface ICartRepository
    {
        void AddToCart(int listingId, int userId);
        void DeleteFromCart(int listingId, int userId);
        List<int> GetIdsInCart(int userId);
        List<Listing> GetAllItemsInCart(int id);
    }
}
