using CarTroubleSolver.Data.Repository.Interfaces;
using CarTroubleSolver.Logic.Services.Interfaces;
using CarTroubleSolver.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Diagnostics;
using CarTroubleSolver.Data.Models.Enums;
using AspNetCoreHero.ToastNotification.Abstractions;

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

        public IActionResult Index(int page = 1, int pageSize = 3)
        {
            if (User.Identity.Name != null)
            {
                var accidents = _accidentService.GetAllFreeAccidents(User.Identity.Name);
                if (accidents.Any())
                {
                    var data = accidents;
                    var totalItems = accidents.Count();
                    var totalPages = (int)Math.Ceiling((double)totalItems / pageSize);
                    var currentPageData = accidents.Skip((page - 1) * pageSize).Take(pageSize).ToList();

                    ViewData["CurrentPage"] = page;
                    ViewData["TotalPages"] = totalPages;
                    ViewData["PageSize"] = pageSize;
                    ViewData["TotalItems"] = totalItems;
                    ViewData["Data"] = currentPageData;

                    return View(accidents);

                }
            }
            
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult FilterAccidents(string severity, string brand)
        {
            var filteredAccidents = _accidentService.Filter(severity, brand, User.Identity.Name).ToList();

            _toastNotification.Warning($"We Found {filteredAccidents.Count} accidents");

            return View("Index", filteredAccidents);
            
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}