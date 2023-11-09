using CarTroubleSolver.Logic.Dto.Cars;
using Microsoft.AspNetCore.Mvc;

namespace CarTroubleSolver.Web.Controllers
{
    public class CarController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AddCar(CarDto car)
        {

            if (ModelState.IsValid)
            {
                return RedirectToAction("Index", "Home");
            }
            return View("Index", car);
        }
    }
}
