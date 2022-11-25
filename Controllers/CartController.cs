using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MikesCars.Interfaces;

namespace MikesCars.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private ICartRepository _cartRepo;
        public CartController(ICartRepository cartRepo)
        {
            _cartRepo = cartRepo;
        }

        [HttpPost("addToCart/{listingId}/{userId}")]
        public void AddToCart(int listingId, int userId)
        {
            _cartRepo.AddToCart(listingId, userId);
        }

        [HttpDelete("deleteFromCart/{listingId}/{userId}")]
        public void DeleteFromCart(int listingId, int userId)
        {
            _cartRepo.DeleteFromCart(listingId, userId);
        }
    }
}
