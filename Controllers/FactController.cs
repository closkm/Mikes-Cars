using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MikesCars.Interfaces;

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
     }
}
