using AspNetCoreHero.ToastNotification.Abstractions;
using CarTroubleSolver.Logic.Dto.Accident;
using CarTroubleSolver.Logic.Services.Interfaces;
using CarTroubleSolver.Web.Models;
using Microsoft.AspNetCore.Mvc;
using PagedList;
using System.Diagnostics;

namespace CarTroubleSolver.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IAccidentService _accidentService;
        private readonly INotyfService _toastNotification;

        public HomeController(ILogger<HomeController> logger, IAccidentService accidentService, INotyfService toastNotification)
        {
            _logger = logger;
            _accidentService = accidentService;
            _toastNotification = toastNotification;
        }

        public IActionResult Index(int? page)
        {

            const int pageSize = 6; 
            var pageNumber = page ?? 1;


            if (User.Identity.Name != null)
            {
                var accidents = _accidentService.GetAllFreeAccidents(User.Identity.Name).ToList();

                var pagedAccidents = accidents.Skip((pageNumber - 1) * pageSize).Take(pageSize);

                var totalAccidentsCount = accidents.Count;

                ViewData["PagedList"] = new StaticPagedList<AccidentAdvertisementDto>(pagedAccidents, pageNumber, pageSize, totalAccidentsCount);

                return View();
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