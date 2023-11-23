using CarTroubleSolver.Logic.Dto.Accident;
using CarTroubleSolver.Logic.Dto.Cars;
using CarTroubleSolver.Logic.Services;
using CarTroubleSolver.Logic.Services.Interfaces;
using CarTroubleSolver.Logic.Validation;
using Microsoft.AspNetCore.Mvc;

namespace CarTroubleSolver.Web.Controllers
{
    public class AccidentController : Controller
    {
        private readonly IAccidentService _accidentService;
        private readonly ICarService _carService;
        public AccidentController(IAccidentService accidentService, ICarService carService)
        {
            _accidentService = accidentService;
            _carService = carService;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult AccidentRequest()
        {
            ViewBag.Cars = _carService.GetUserCars<CarFormDto>(User.Identity.Name);
            return View();
        }

        public IActionResult SendRequest(WebAccidentRequestDto accident)
        {
            var validator = new WebAccidentRequestDtoValidator();
            var validationResult = validator.Validate(accident);
            if (!validationResult.IsValid)
            {
                foreach (var error in validationResult.Errors)
                {
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                }
                ViewBag.Cars = _carService.GetUserCars<CarFormDto>(User.Identity.Name);
                return View("AccidentRequest", accident);
            }
            else
            {
                _accidentService.AddAccident(accident, User.Identity.Name);
                return RedirectToAction("Index", "Home");
            }
            
        }
        public IActionResult AccidentHistory()
        {
            ViewBag.Applicant = _accidentService.ShowHistoryOfAccidentsApplicant(User.Identity.Name);
            ViewBag.Asignee = _accidentService.ShowHistoryOfAccidentsAsignee(User.Identity.Name);

            return View();
        }

        public IActionResult AccidentDetails(Guid accidentId)
        {
            var accident = _accidentService.GetAllFreeAccidents(User.Identity.Name).FirstOrDefault(a => a.Id == accidentId);
            return View(accident);
        }

        public IActionResult ApplyForAccidentHelp(AccidentAdvertisementDto accident)
        {
            _accidentService.HelpInAccident(User.Identity.Name, accident.Id);
            return RedirectToAction("Index", "Home");
        }
      
    }
}
