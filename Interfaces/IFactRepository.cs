using MikesCars.Models;

namespace MikesCars.Interfaces
{
    public interface IFactRepository
    {
        void PostFacts(Fact fact);
    }
}
