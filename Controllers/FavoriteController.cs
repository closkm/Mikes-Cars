using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MikesCars.Interfaces;

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
    }
}
