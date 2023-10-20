using CarTroubleSolver.Data.Repository.Interfaces;
using CarTroubleSolver.Logic.Services.Interfaces;
using TheCarMarket.Data.Models;

namespace CarTroubleSolver.Logic.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public void Add(User user)
        {
            _userRepository.Add(user);
        }
    }
}
