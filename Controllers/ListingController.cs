using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MikesCars.Interfaces;
using MikesCars.Models;

namespace MikesCars.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ListingController : ControllerBase
    {
        private IListingRepository _listingRepo;
        private IFactRepository _factRepo;
        private ICartRepository _cartRepo;
        private IImageRepository _imageRepo;
        private IFavoriteRepository _favoriteRepo;
        public ListingController(IListingRepository listingRepo, IFactRepository factRepo, ICartRepository cartRepo, IImageRepository imageRepo, IFavoriteRepository favoriteRepo)
        {
            _listingRepo = listingRepo;
            _factRepo = factRepo;
            _cartRepo = cartRepo;
            _imageRepo = imageRepo;
            _favoriteRepo = favoriteRepo;
        }


        [HttpGet("GetAvailableListings/{userId}")]
        public List<Listing> GetAllUsers(int userId)
        {
            return _listingRepo.GetAllAvailableListings(userId);
        }

        [HttpGet("CheckIfUsersCar/{userId}/{listingId}")]
        public bool CheckIfUsersCar(int userId, int listingId)
        {
            return _listingRepo.CheckIfUsersCar(userId, listingId);
        }

        [HttpGet("{id}")]
        public Listing GetListingById(int id)
        {
            return _listingRepo.GetListingById(id);
        }

        [HttpPost("NewListing")]
        //needs to return int
        public int PostNewListing(ListingFact listingfact)
        {
            Listing listing = new Listing()
            {
                id = listingfact.id,
                userId = listingfact.userId,
                type = listingfact.type,
                maker = listingfact.maker,
                address = listingfact.address,
                price = listingfact.price,
                dateOfListing = listingfact.dateOfListing,
                favorites = listingfact.favorites,
                purchased = listingfact.purchased,
                inCart = listingfact.inCart
            };

            Fact fact = new Fact()
            {
                id = listingfact.id,
                electric = listingfact.electric,
                listingId = listingfact.id,
                mpg = listingfact.mpg,
                crashes = listingfact.crashes,
                miles = listingfact.miles,
                warranty = listingfact.warranty
            };

            int id = _listingRepo.PostNewListing(listing);
            fact.listingId = id;
            
            for(int i = 0; i < listingfact.images.Length; i++)
            {
                ImageModel image = new ImageModel()
                {
                    Id = 0,
                    Img = listingfact.images[i],
                    ListingId = id,
                    DisplayOrder = i + 1,
                };
                _imageRepo.PostNewImage(image);
            }

            _factRepo.PostFacts(fact);
            return id;
        }

        [HttpPut("NewListing")]
        public void EditListing(Listing listing)
        {
            _listingRepo.EditListing(listing);
        }

        [HttpPut("Purchased/{userId}/{listingId}")]
        public void Purchased(int userId, int listingId)
        {
            _listingRepo.Purchased(listingId);
            _cartRepo.Purchased(userId, listingId);
        }

        [HttpDelete("DeleteListing/{listingId}")]

        public void DeleteListing(int listingId)
        {
            _factRepo.DeleteFact(listingId);
            _cartRepo.DeleteCart(listingId);
            _imageRepo.DeleteImage(listingId);
            _favoriteRepo.DeleteFavorite(listingId);
            _listingRepo.DeleteListing(listingId);
        }
    }
}
