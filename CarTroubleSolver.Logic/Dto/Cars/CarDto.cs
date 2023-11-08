using CarTroubleSolver.Data.Models.Enums;
using CarTroubleSolver.Data.Models;
using System.ComponentModel.DataAnnotations;

namespace CarTroubleSolver.Logic.Dto.Cars
{
    public class CarDto
    {
        public CarBrand Brand { get; set; }

        [Required(ErrorMessage = "Specify Car Model.")]
        public string CarModels { get; set; }

        [Required(ErrorMessage = "Specify Engine type.")]
        public string EngineType { get; set; }
        public FuelType FuelType { get; set; }

        [Required(ErrorMessage = "Specify Door Count.")]
        public int DoorCount { get; set; }

        [Required(ErrorMessage = "Specify Mileage.")]
        public int Mileage { get; set; }

        [Required(ErrorMessage = "Specify Color.")]
        public string Color { get; set; }
    }
}
