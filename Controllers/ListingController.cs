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
        public ListingController(IListingRepository listingRepo, IFactRepository factRepo, ICartRepository cartRepo)
        {
            _listingRepo = listingRepo;
            _factRepo = factRepo;
            _cartRepo = cartRepo;
        }


        [HttpGet]
        public List<Listing> GetAllUsers()
        {
            return _listingRepo.GetAllAvailableListings();
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
    }
}
