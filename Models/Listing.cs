namespace MikesCars.Models
{
    public class Listing
    {
        public int id { get; set; }
        public int userId { get; set; }
        public String type { get; set; }
        public String maker { get; set; }
        public String address { get; set; }
        public double price { get; set; }
        public DateTime dateOfListing { get; set; }
        public int favorites { get; set; }
        public bool purchased { get; set; }
        public bool inCart { get; set; }

    }
}
