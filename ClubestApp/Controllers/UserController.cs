﻿namespace ClubestApp.Controllers
{
    using ClubestApp.Areas.Identity.Pages.Account;
    using ClubestApp.Common;
    using ClubestApp.Data.Models;
    using ClubestApp.Models.BindingModels;
    using ClubestApp.Models.InputModels;
    using ClubestApp.Services;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using System.Security.Claims;
    using System.Threading.Tasks;

    public class UserController : Controller
    {
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly UserService userService;

        public UserController(
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            ILogger<RegisterModel> logger,
            UserService userService)
        {
            this._userManager = userManager;
            this._signInManager = signInManager;
            this._logger = logger;
            this.userService = userService;
        }

        public IActionResult Profile()
        {
            //Finds user by his id
            string id = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            User user = this.userService.FindUserById(id);

            EditProfileBindingModel model = new EditProfileBindingModel()
            {
                Email = user.Email,
                Username = user.UserName,
                FirstName = user.FirstName,
                LastName = user.LastName,
                PhoneNumber = user.PhoneNumber,
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult Profile(EditProfileInputModel model)
        {
            if (ModelState.IsValid)
            {
                this.userService.EditUser(model);
            }

            return this.Redirect("/User/Profile");
        }
        public IActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(PasswordInputModel inputModel)
        {
            if (ModelState.IsValid)
            {
                User user = await _userManager.GetUserAsync(User);

                var changePasswordResult = await _userManager.ChangePasswordAsync(user, inputModel.OldPassword, inputModel.Password);
                if (!changePasswordResult.Succeeded)
                {
                    await _signInManager.RefreshSignInAsync(user);
                    ViewData["Message"] = "Успешно сменихте паролата си!";
                }
            }
            return this.View("ChangePassword");
        }

        public IActionResult DownloadData()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterInputModel inputModel)
        {
            if (ModelState.IsValid)
            {
                var user = new User
                {
                    UserName = inputModel.Email,
                    Email = inputModel.Email,
                    FirstName = inputModel.FirstName,
                    LastName = inputModel.LastName,
                };
                var result = await _userManager.CreateAsync(user, inputModel.Password);
                if (result.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return Redirect("/");
                }
                else
                {
                    ModelState.AddModelError(UserFields.Email, ErrorMessages.DublicateEmail);
                    return this.View();
                }
            }

            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginInputModel inputModel)
        {
            if (ModelState.IsValid)
            {
                var result = await this._signInManager.PasswordSignInAsync(inputModel.Email, inputModel.Password, true, lockoutOnFailure: true);
                if (result.Succeeded)
                {
                    _logger.LogInformation("User logged in.");
                    return Redirect("/");
                }

                ModelState.AddModelError(UserFields.Email, ErrorMessages.InvalidEmailOrPassword);
                return this.View();
            }

            ModelState.AddModelError(UserFields.Email, ErrorMessages.InvalidEmailOrPassword);
            return this.View();
        }
    }
}