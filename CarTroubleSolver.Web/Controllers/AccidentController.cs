using AspNetCoreHero.ToastNotification.Abstractions;
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
        private readonly INotyfService _toastNotification;
        public AccidentController(IAccidentService accidentService, ICarService carService, INotyfService toastNotification)
        {
            _accidentService = accidentService;
            _carService = carService;
            _toastNotification = toastNotification;
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
                _toastNotification.Warning("Fill All Inputs");
                return View("AccidentRequest", accident);
            }
            else
            {
                _accidentService.AddAccident(accident, User.Identity.Name);
                _toastNotification.Success("Accident Added Sucessfully");
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
            _toastNotification.Success("Your request for assistance has been granted");
            _toastNotification.Information($"You have pledged to provide assistance to {accident.ApplicantUserInfo.Name} {accident.ApplicantUserInfo.Surname} enter your story to contact with {accident.ApplicantUserInfo.Name} {accident.ApplicantUserInfo.Surname}",12);

            return RedirectToAction("Index", "Home");
        }
      
    }
}
