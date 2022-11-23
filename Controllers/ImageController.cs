using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MikesCars.Interfaces;

namespace MikesCars.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        private IImageRepository _imageRepo;
        public ImageController(IImageRepository imageRepo)
        {
            _imageRepo = imageRepo;
        }
    }
}
