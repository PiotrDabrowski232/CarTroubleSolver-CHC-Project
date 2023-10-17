

using CarTroubleSolver.Data.Models.Enums;
using TheCarMarket.Data.Models.Enums;

namespace TheCarMarket.Data.Models
{
    public class Car
    {
        public Guid Id { get; set; }
        public CarBrand Brand { get; set; }
        public string CarModels { get; set; }
        public FuelType FuelType { get; set; }
        public int DoorCount { get; set; }
        public int Mileage {  get; set; } 
    }
}
