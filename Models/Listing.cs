namespace MikesCars.Models
{
    public class Listing
    {
        public int id { get; set; }
        public int userId { get; set; }
        public String type { get; set; }
        public String maker { get; set; }
        public String Address { get; set; }
        public int price { get; set; }
        public DateOnly dateOfListing { get; set; }
        public int favorites { get; set; }
        public bool purchased { get; set; }
        public bool inCart { get; set; }

    }
}
