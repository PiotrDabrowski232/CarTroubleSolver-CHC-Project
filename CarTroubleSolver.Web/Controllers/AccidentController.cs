using Microsoft.AspNetCore.Mvc;

namespace CarTroubleSolver.Web.Controllers
{
    public class AccidentController : Controller
    {
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
            return View();
        }
    }
}
