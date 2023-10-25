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
            car.OwnerId = _userRepository.GetUserByEmail(userEmail).Id;
            car.Id = Guid.NewGuid();
            _carRepository.Add(car);
        }

        public IEnumerable<CarDto> GetUserCars(string userEmail)
        {
            var cars = GetAll();
            var user = _userRepository.GetUserByEmail(userEmail);
            cars = cars.Where(u => u.OwnerId == user.Id);
            return _mapper.Map<IEnumerable<CarDto>>(cars);
        }

        public void DeleteCarFromUserCollection(CarDto carToDelete, string userEmail)
        {
            var user = _userRepository.GetUserByEmail(userEmail);
            var car = GetAll().Where(u => u.OwnerId == user.Id).FirstOrDefault(c => c.Brand == carToDelete.Brand && c.CarModels == carToDelete.CarModels && c.Mileage == carToDelete.Mileage);
            _carRepository.Remove(car);
        }
    }
}
