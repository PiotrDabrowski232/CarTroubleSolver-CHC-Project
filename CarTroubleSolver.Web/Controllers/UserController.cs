using CarTroubleSolver.Logic.Dto.User;
using CarTroubleSolver.Logic.Services.Interfaces;
using CarTroubleSolver.Logic.Validation;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CarTroubleSolver.Web.Controllers
{
    public class UserController : Controller
    {

        private readonly IUserService _userService;
        private readonly ICarService _carService;

        public UserController(IUserService userService, ICarService carService)
        {
            _userService = userService;
            _carService = carService;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Register()
        {
            return View();
        }

        public IActionResult RegisteredUser(RegisterUserDto user)
        {
            var validator = new RegisterUserDtoValidator();
            var validationResult = validator.Validate(user);
            if (!validationResult.IsValid)
            {
                foreach (var error in validationResult.Errors)
                {
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                }
                return View("Register", user);
            }
            else
            {
                _userService.Add(user);
                return RedirectToAction("Index", "Home");
            }
        }

        public IActionResult Login()
        {
            return View();
        }

        public IActionResult LoginRequest(string Email, string Password)
        {

           if (_userService.VerifyUserInputs(Email, Password))
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return RedirectToAction("Login");
            }
            
        }

        public IActionResult Profile()
        {
            var user = _userService.GetLoggedInUser("pp@o2.pl");

            ViewBag.Cars = _carService.GetUserCars(user.Email);
            return View(user);
        }
    }
}
