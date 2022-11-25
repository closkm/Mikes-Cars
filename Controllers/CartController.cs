﻿using Microsoft.AspNetCore.Http;
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

        [HttpPost("{listingId}/{userId}")]
        public void AddToCart(int listingId, int userId)
        {
            _cartRepo.AddToCart(listingId, userId);
        }
    }
}
