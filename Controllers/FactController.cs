using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MikesCars.Interfaces;
using MikesCars.Models;

namespace MikesCars.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FactController : ControllerBase
    {
        private IFactRepository _factRepo;
        public FactController(IFactRepository factRepo)
        {
            _factRepo = factRepo;
        }

        [HttpPost]
        public void PostFacts(Fact fact)
        {
            _factRepo.PostFacts(fact);
        }

        [HttpGet("{listingId}")]
        public Fact GetFacts(int listingId)
        {
            return _factRepo.GetFacts(listingId);
        }

        [HttpPut("EditFacts")]
        public void EditFacts(Fact fact)
        {
            _factRepo.EditFacts(fact);
        }
    }
}
