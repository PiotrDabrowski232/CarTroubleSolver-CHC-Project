﻿

using CarTroubleSolver.Data.Models.Enums;
using TheCarMarket.Data.Models.Enums;

namespace TheCarMarket.Data.Models
{
    public class Car
    {
        public Car()
        {
            Id = Guid.NewGuid();
        }
        public Guid Id { get; set; }
        public Guid OwnerId { get; set; }
        public User Owner { get; set; }
        public CarBrand Brand { get; set; }
        public string CarModels { get; set; }
        public string EngineType { get; set; }
        public FuelType FuelType { get; set; }
        public int DoorCount { get; set; }
        public int Mileage { get; set; }
        public string Color { get; set; }
    }
}
