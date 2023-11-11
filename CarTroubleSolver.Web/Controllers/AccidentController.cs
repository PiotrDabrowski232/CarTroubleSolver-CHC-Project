using CarTroubleSolver.Logic.Dto.Accident;
using CarTroubleSolver.Logic.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CarTroubleSolver.Web.Controllers
{
    public class AccidentController : Controller
    {
        private readonly IAccidentService _accidentService;
        public AccidentController(IAccidentService accidentService)
        {
            _accidentService = accidentService;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult AccidentRequest()
        {
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
            var accident = _accidentService.GetAccident(accidentId);
            return View(accident);
        }
    }
}
