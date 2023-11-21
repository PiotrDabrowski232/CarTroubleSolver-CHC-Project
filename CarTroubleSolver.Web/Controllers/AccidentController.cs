using CarTroubleSolver.Logic.Dto.Accident;
using CarTroubleSolver.Logic.Dto.Cars;
using CarTroubleSolver.Logic.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

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
            
            _accidentService.AddAccident(accident, User.Identity.Name);
            return View();
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
