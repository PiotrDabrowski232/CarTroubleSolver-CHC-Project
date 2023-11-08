using CarTroubleSolver.Data.Models.Enums;
using CarTroubleSolver.Data.Models;

namespace CarTroubleSolver.Logic.Dto.Cars
{
    public class CarDto
    {
        public CarBrand Brand { get; set; }
        public string CarModels { get; set; }
        public string EngineType { get; set; }
        public FuelType FuelType { get; set; }
        public int DoorCount { get; set; }
        public int Mileage { get; set; }
        public string Color { get; set; }
    }
}
