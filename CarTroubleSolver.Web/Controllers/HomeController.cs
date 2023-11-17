using CarTroubleSolver.Data.Repository.Interfaces;
using CarTroubleSolver.Logic.Services.Interfaces;
using CarTroubleSolver.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Diagnostics;

namespace CarTroubleSolver.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IAccidentService _accidentService;

        public HomeController(ILogger<HomeController> logger, IAccidentService accidentService)
        {
            _logger = logger;
            _accidentService = accidentService;
        }

        public IActionResult Index()
        {
            var accidents = _accidentService.GetAllFreeAccidents("pp@o2.pl");
            if (accidents.Any())
            {
                return View(accidents);

            }
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}