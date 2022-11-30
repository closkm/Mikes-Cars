using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MikesCars.Interfaces;
using MikesCars.Models;
using Newtonsoft.Json;

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


        [HttpGet("{listingId}")]
        public List<ImageModel> GetListingImages(int listingId)
        {
            return _imageRepo.GetListingImages(listingId);
        }

        [HttpPost]
        public void PostNewImage(object michael)
        {
            ImageModel imageModel = JsonConvert.DeserializeObject<ImageModel>(michael.ToString());
            _imageRepo.PostNewImage(imageModel);
        }

    }
}
