using MikesCars.Models;

namespace MikesCars.Interfaces
{
    public interface IFactRepository
    {
        void PostFacts(Fact fact);
        Fact GetFacts(int listingId);
        void DeleteFact(int listingId);
    }
}
