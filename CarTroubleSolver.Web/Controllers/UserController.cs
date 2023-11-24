using AspNetCoreHero.ToastNotification.Abstractions;
using CarTroubleSolver.Logic.Dto.Cars;
using CarTroubleSolver.Logic.Dto.User;
using CarTroubleSolver.Logic.Services.Interfaces;
using CarTroubleSolver.Logic.Validation;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Web.Helpers;

namespace CarTroubleSolver.Web.Controllers
{
    public class UserController : Controller
    {

        private readonly IUserService _userService;
        private readonly ICarService _carService;
        private readonly INotyfService _toastNotification;


        public UserController(IUserService userService, ICarService carService, INotyfService toastNotification)
        {
            _userService = userService;
            _carService = carService;
            _toastNotification = toastNotification;
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
                _toastNotification.Warning("Fill All Inputs Correctly");

                return View("Register", user);
            }
            else
            {
                _userService.Add(user);
                _toastNotification.Success("The account was created correctly");
                return RedirectToAction("Index", "Home");
            }
        }

        public IActionResult Login()
        {
            return View();
        }

        public async Task<IActionResult> LoginRequest(string Email, string Password)
        {

            if (_userService.VerifyUserInputs(Email, Password))
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, Email)
                };

                var claimIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                var authProperties = new AuthenticationProperties
                {
                    IsPersistent = true,
                };

                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimIdentity), authProperties);

                _toastNotification.Information($"Welcome {Email} You Have Logged Correctly");
                return RedirectToAction("Index", "Home");
            }
            else
            {
                _toastNotification.Error($"There is no such password or user");
                return RedirectToAction("Login");
            }

        }

        public IActionResult Profile()
        {
            var user = _userService.GetLoggedInUser(User.Identity.Name);

            ViewBag.Cars = _carService.GetUserCars<CarDto>(User.Identity.Name);
            return View(user);
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            _toastNotification.Information($"You Have Logged Out Correctly");
            return RedirectToAction("Index", "Home");
        }
    }
}
