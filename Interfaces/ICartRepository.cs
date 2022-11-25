namespace MikesCars.Interfaces
{
    public interface ICartRepository
    {
        void AddToCart(int listingId, int userId);
    }
}
