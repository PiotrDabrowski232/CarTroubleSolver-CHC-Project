using AspNetCoreHero.ToastNotification.Abstractions;
using CarTroubleSolver.Logic.Dto.Cars;
using CarTroubleSolver.Logic.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CarTroubleSolver.Web.Controllers
{
    public class CarController : Controller
    {
        private readonly ICarService _carService;
        private readonly INotyfService _toastNotification;


        public CarController(ICarService carService, INotyfService toastNotification)
        {
            _carService = carService;
            _toastNotification = toastNotification;
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
                _toastNotification.Success("Car Added Successfully");
                return RedirectToAction("Profile", "User");
            }
            _toastNotification.Warning("Fill All Inputs Correctly");
            return View("Index", car);
        }

        public IActionResult UserCarDelete()
        {
            var cars = _carService.GetUserCars<CarDto>(User.Identity.Name);
            _toastNotification.Information("Choose Car To Delete");
            return View(cars);
        }

        public IActionResult DeleteCar(CarDto car)
        {
            _carService.DeleteCarFromUserCollection(car, User.Identity.Name);
            _toastNotification.Success("Car Deleted Successfully");
            return RedirectToAction("Profile", "User");
        }
    }
}
