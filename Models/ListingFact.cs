namespace MikesCars.Models
{
    public class ListingFact
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

        public bool electric { get; set; }
        public int listingId { get; set; }
        public double mpg { get; set; }
        public int crashes { get; set; }
        public int miles { get; set; }
        public bool warranty { get; set; }


        public string[] images { get; set; }
    }
}
