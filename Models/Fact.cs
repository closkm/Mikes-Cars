namespace MikesCars.Models
{
    public class Fact
    {
        public int id { get; set; }
        public bool electric { get; set; }
        public int listingId { get; set; }
        public double mpg { get; set; }
        public int crashes { get; set; }
        public int miles { get; set; }
        public bool warranty { get; set; }
    }
}
