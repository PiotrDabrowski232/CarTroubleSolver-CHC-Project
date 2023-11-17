using AutoMapper;
using CarTroubleSolver.Data.Repository.Interfaces;
using CarTroubleSolver.Logic.Dto.User;
using CarTroubleSolver.Logic.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using TheCarMarket.Data.Models;

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
            var user = GetUsers().FirstOrDefault(u => u.Email == email);

            return _mapper.Map<LogedInUserDto>(user);
        }

        public bool VerifyUserInputs(string email, string password)
        {
            try
            {

                var user = GetUsers().FirstOrDefault(u => u.Email == email);


                if (user is null)
                {
                    return false;
                }

                var result = _passwordHasher.VerifyHashedPassword(user, user.Password, password);

                if (result == PasswordVerificationResult.Failed)
                {
                    return false;
                }
                else
                {
                    return true;
                }

            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
