﻿using CarTroubleSolver.Logic.Dto.User;
using CarTroubleSolver.Logic.Services.Interfaces;
using CarTroubleSolver.Logic.Validation;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CarTroubleSolver.Web.Controllers
{
    public class UserController : Controller
    {

        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
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

            _userService.Add(user);
            return RedirectToAction("Register");
        }
    }
}