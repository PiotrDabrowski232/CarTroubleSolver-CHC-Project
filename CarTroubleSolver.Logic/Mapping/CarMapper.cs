using AutoMapper;
using CarTroubleSolver.Logic.Dto.Cars;
using TheCarMarket.Data.Models;

namespace CarTroubleSolver.Logic.Mapping
{
    public class CarMapper : Profile
    {
        public CarMapper()
        {
            CreateMap<Car, CarDto>();
            CreateMap<CarDto, Car>();
        }

    }
}
