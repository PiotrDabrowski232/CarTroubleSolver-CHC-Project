using AutoMapper;
using CarTroubleSolver.Data.Repository.Interfaces;
using CarTroubleSolver.Logic.Dto;
using CarTroubleSolver.Logic.Services.Interfaces;
using TheCarMarket.Data.Models;

namespace CarTroubleSolver.Logic.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        public UserService(IUserRepository userRepository, IMapper _mapper)
        {
            _userRepository = userRepository;
        }

        public void Add(RegisterUserDto user)
        {
            _userRepository.Add(_mapper.Map<User>(user));
        }
    }
}
