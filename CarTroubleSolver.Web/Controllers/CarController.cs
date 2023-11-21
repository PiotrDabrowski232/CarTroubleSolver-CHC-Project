using CarTroubleSolver.Logic.Dto.Cars;
using CarTroubleSolver.Logic.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CarTroubleSolver.Web.Controllers
{
    public class CarController : Controller
    {
        private readonly ICarService _carService;

        public CarController(ICarService carService)
        {
            _carService = carService;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AddCar(CarDto car)
        {
            if (ModelState.IsValid)
            {
                _carService.Add(car, User.Identity.Name);
                return RedirectToAction("Profile", "User");
            }
            return View("Index", car);
        }

        public IActionResult UserCarDelete()
        {
            var cars = _carService.GetUserCars<CarDto>(User.Identity.Name);
            return View(cars);
        }

        public IActionResult DeleteCar(CarDto car)
        {
            _carService.DeleteCarFromUserCollection(car, User.Identity.Name);
            return RedirectToAction("Profile", "User");
        }
    }
}
