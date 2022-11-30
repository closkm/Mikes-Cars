using MikesCars.Models;

namespace MikesCars.Interfaces
{
    public interface IImageRepository
    {
        List<ImageModel> GetListingImages(int listingId);
        void PostNewImage(ImageModel image);
    }
}
