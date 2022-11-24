using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MikesCars.Interfaces;
using MikesCars.Models;

namespace MikesCars.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUserRepository _userRepo;
        public UserController(IUserRepository userRepo)
        {
            _userRepo = userRepo;
        }

        [HttpGet]
        public List<User> GetAllUsers()
        {
            return _userRepo.GetAllUsers();
        }

        [HttpGet("{firebaseId}")]
        public User GetLoggedInUser(string firebaseId)
        {
            return _userRepo.GetLoggedInUser(firebaseId);
        }

        [HttpPost]
        public void EditLoggedInUser(User user)
        {
            _userRepo.EditLoggedInUser(user);
        }
    }
}
