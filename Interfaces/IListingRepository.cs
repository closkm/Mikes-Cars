using MikesCars.Models;

namespace MikesCars.Interfaces
{
    public interface IListingRepository
    {
        List<Listing> GetAllAvailableListings();
        Listing GetListingById(int id);
        int PostNewListing(Listing listing);
        void EditListing(Listing listing);
        void UpdateFavorites(int listingId);
        void DeleteFromFavorite(int listingId);
        void AddToCart(int listingId);
        void DeleteFromCart(int listingId);
        void Purchased(int listingId);
        bool CheckIfUsersCar(int userId, int listingId);
        void DeleteListing(int listingId);
    }
}
