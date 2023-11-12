using CarTroubleSolver.Logic.Dto.Accident;
using CarTroubleSolver.Logic.Services.Interfaces;
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
            ViewBag.Cars = _carService.GetUserCars("pp@o2.pl");
            return View();
        }

        public IActionResult AccidentHistory()
        {
            ViewBag.Applicant = _accidentService.ShowHistoryOfAccidentsApplicant("pp@o2.pl");
            ViewBag.Asignee = _accidentService.ShowHistoryOfAccidentsAsignee("pp@o2.pl");

            return View();
        }

        public IActionResult AccidentDetails(Guid accidentId)
        {
            var accident = _accidentService.GetAllFreeAccidents("pp@o2.pl").FirstOrDefault(a => a.Id == accidentId);
            return View(accident);
        }

        public IActionResult ApplyForAccidentHelp(AccidentAdvertisementDto accident)
        {
            _accidentService.HelpInAccident("pp@o2.pl", accident.Id);
            return RedirectToAction("Index", "Home");
        }
    }
}
