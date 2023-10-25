using AutoMapper;
using CarTroubleSolver.Data.Repository.Interfaces;
using CarTroubleSolver.Logic.Dto.User;
using CarTroubleSolver.Logic.Services.Interfaces;
using TheCarMarket.Data.Models;

namespace CarTroubleSolver.Logic.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }
        private IEnumerable<User> GetUsers()
        {
            return _userRepository.GetAll();
        }

        public void Add(RegisterUserDto user)
        {
            _userRepository.Add(_mapper.Map<User>(user));
        }

        public LogedInUserDto GetLoggedInUser(string email)
        {
            var user = GetUsers().FirstOrDefault(u => u.Email==email);

            return _mapper.Map<LogedInUserDto>(user);
        }

        public bool VerifyUserInputs(string email, string password)
        {
            var user = GetUsers().FirstOrDefault(u => u.Email == email);

            if (user == null || user.Password != password)
                return false;
            return true;
        }
    }
}
