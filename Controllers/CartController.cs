using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MikesCars.Interfaces;
using MikesCars.Models;

namespace MikesCars.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private ICartRepository _cartRepo;
        private IListingRepository _listingRepo;
        public CartController(ICartRepository cartRepo, IListingRepository listingRepo)
        {
            _cartRepo = cartRepo;
            _listingRepo = listingRepo;
        }

        [HttpPost("addToCart/{listingId}/{userId}")]
        public void AddToCart(int listingId, int userId)
        {
            _cartRepo.AddToCart(listingId, userId);
            _listingRepo.AddToCart(listingId);
        }

        [HttpDelete("deleteFromCart/{listingId}/{userId}")]
        public void DeleteFromCart(int listingId, int userId)
        {
            _cartRepo.DeleteFromCart(listingId, userId);
            _listingRepo.DeleteFromCart(listingId);
        }

        [HttpGet("{userId}")]
        public List<Listing> GetAllItemsInCart(int userId)
        {
            List<int> ints = _cartRepo.GetIdsInCart(userId);
            List<Listing> listings = new List<Listing>();

            foreach (int id in ints)
            {
                Listing listing = _cartRepo.GetAllItemsInCart(id)[0];
                listings.Add(listing);
            }
            return listings;
        }
    }
}
