using System.Drawing;

namespace MikesCars.Models
{
    public class ImageModel
    {
        public int Id { get; set; }
        public Image Img { get; set; }
        public int ListingId { get; set; }
        public int DisplayOrder { get; set; }
    }
}
