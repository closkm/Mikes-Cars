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
     }
}
