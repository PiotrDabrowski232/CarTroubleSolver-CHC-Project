using CarTroubleSolver.Logic.Dto.Cars;
using CarTroubleSolver.Logic.Dto.User;
using CarTroubleSolver.Logic.Services.Interfaces;
using CarTroubleSolver.Logic.Validation;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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

                return RedirectToAction("Index", "Home");
            }
            else
            {
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
            return RedirectToAction("Index", "Home");
        }
    }
}
