using MikesCars.Models;

namespace MikesCars.Interfaces
{
    public interface IListingRepository
    {
        List<Listing> GetAllAvailableListings();
        Listing GetListingById(int id);
        void PostNewListing(Listing listing);
        void EditListing(Listing listing);
        void UpdateFavorites(int listingId);
    }
}
