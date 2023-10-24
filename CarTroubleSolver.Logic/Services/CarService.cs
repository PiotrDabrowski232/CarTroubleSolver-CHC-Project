using AutoMapper;
using CarTroubleSolver.Data.Repository;
using CarTroubleSolver.Data.Repository.Interfaces;
using CarTroubleSolver.Logic.Dto.Cars;
using CarTroubleSolver.Logic.Services.Interfaces;
using TheCarMarket.Data.Models;

namespace CarTroubleSolver.Logic.Services
{
    public class CarService : ICarService
    {
        private readonly ICarRepository _carRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        public CarService(ICarRepository carRepository, IMapper mapper, IUserRepository userRepository)
        {
            _carRepository = carRepository;
            _mapper = mapper;
            _userRepository = userRepository;
        }
        private IEnumerable<Car> GetAll()
        {
            return _carRepository.GetAll();
        }

        public void Add(CarDto carDto, string userEmail)
        {
            var car = _mapper.Map<Car>(carDto);
            car.Owner = _userRepository.GetUserByEmail(userEmail);
            _carRepository.Add(car);
        }

        public IEnumerable<CarDto> GetUserCars(string userEmail)
        {
            var cars = GetAll().Where(u => u.Owner.Id == _userRepository.GetUserByEmail(userEmail).Id);
            return _mapper.Map<IEnumerable<CarDto>>(cars);
        }
    }
}
