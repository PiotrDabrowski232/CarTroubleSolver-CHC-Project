using CarTroubleSolver.Data.Models.Enums;
using CarTroubleSolver.Logic.Dto.Cars;
using System.Web.Mvc;

namespace CarTroubleSolver.Logic.Dto.Accident
{
    public class WebAccidentRequestDto
    {

        public CarFormDto? SelectedCar { get; set; }
        public CollisionSeverity CollisionSeverity { get; set; }
        public string AccidentDescription { get; set; }

    }
}
