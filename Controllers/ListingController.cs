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
        public ListingController(IListingRepository listingRepo)
        {
            _listingRepo = listingRepo;
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
        public void PostNewListing(Listing listing)
        {
            _listingRepo.PostNewListing(listing);
        }

        [HttpPut("NewListing")]
        public void EditListing(Listing listing)
        {
            _listingRepo.EditListing(listing);
        }

        [HttpPut("Purchased/{listingId}")]
        public void Purchased(int listingId)
        {
            _listingRepo.Purchased(listingId);
        }
    }
}
