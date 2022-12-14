using MikesCars.Models;

namespace MikesCars.Interfaces
{
    public interface IUserRepository
    {
        List<User> GetAllUsers();
        User GetLoggedInUser(string firebaseId);
        void EditLoggedInUser(User user);
        bool CheckForUser(string firebaseId);
        void RegisterUser(User user);
    }
}
