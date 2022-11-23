namespace MikesCars.Models
{
    public class Cart
    {
        public int id { get; set; }
        public int userId { get; set; }
        public int listingId { get; set; }
        public bool purchase { get; set; }
    }
}
