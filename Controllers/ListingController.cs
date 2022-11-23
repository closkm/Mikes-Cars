using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MikesCars.Interfaces;

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
    }
}
