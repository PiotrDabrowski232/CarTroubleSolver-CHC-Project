using CarTroubleSolver.Data.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheCarMarket.Data.Models.Enums;

namespace CarTroubleSolver.Logic.Dto.Cars
{
    public class CarDto
    {
        public Guid Owner { get; set; }
        public CarBrand Brand { get; set; }
        public string CarModels { get; set; }
        public string EngineType { get; set; }
        public FuelType FuelType { get; set; }
        public int DoorCount { get; set; }
        public int Mileage { get; set; }
        public string Color { get; set; }
    }
}
