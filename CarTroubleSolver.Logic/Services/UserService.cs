using AutoMapper;
using CarTroubleSolver.Data.Repository.Interfaces;
using CarTroubleSolver.Logic.Dto.User;
using CarTroubleSolver.Logic.Services.Interfaces;
using TheCarMarket.Data.Models;
using Microsoft.AspNetCore.Identity;

namespace CarTroubleSolver.Logic.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IPasswordHasher<User> _passwordHasher;
        public UserService(IUserRepository userRepository, IMapper mapper, IPasswordHasher<User> passwordHasher)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _passwordHasher = passwordHasher;
        }
        private IEnumerable<User> GetUsers()
        {
            return _userRepository.GetAll();
        }

        public void Add(RegisterUserDto user)
        {
            user.Password = _passwordHasher.HashPassword(null, user.Password);
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


            if (user is null )
            {
                throw new Exception("invalid username or password");
            }

            var result = _passwordHasher.VerifyHashedPassword(user, user.Password, password);

            if (result == PasswordVerificationResult.Failed)
            {
                throw new Exception("invalid username or password");
            }
            else
            {
                return true;
            }
        }
    }
}
