using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MikesCars.Interfaces;
using MikesCars.Models;

namespace MikesCars.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FavoriteController : ControllerBase
    {
        private IFavoriteRepository _favoriteRepo;
        private IListingRepository _listingRepo;
        public FavoriteController(IFavoriteRepository favoriteRepo, IListingRepository listingRepo)
        {
            _favoriteRepo = favoriteRepo;
            _listingRepo = listingRepo;
        }

        [HttpPost("{userId}/{listingId}")]
        public void AddFavorite(int userId, int listingId)
        {
            _favoriteRepo.AddToFavTable(userId, listingId);
            _listingRepo.UpdateFavorites(listingId);
        }

        [HttpDelete("DeleteFromFavorite/{userId}/{listingId}")]
        public void DeleteFromFavorite(int userId, int listingId)
        {
            _favoriteRepo.DeleteFromFavorite(userId, listingId);
            _listingRepo.DeleteFromFavorite(listingId);
        }

        [HttpGet("GetUserFavorites/{userId}")]
        public List<Listing> GetUserFavorites(int userId)
        {
            List<int> listingIds = _favoriteRepo.GetIdsInFav(userId);
            List<Listing> listings = new List<Listing>();

            foreach(int id in listingIds)
            {
                Listing listing = _favoriteRepo.GetAllItemsInFavorite(id)[0];
                listings.Add(listing);
            }
            return listings;
        }

        [HttpGet("CheckIfFav/{userId}/{listingId}")]

        public bool CheckIfFav(int userId, int listingId)
        {
            return _favoriteRepo.CheckIfFav(userId, listingId);
        }
    }
}
