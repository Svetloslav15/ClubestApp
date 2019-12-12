namespace ClubestApp.Controllers
{
    using ClubestApp.Areas.Identity.Pages.Account;
    using ClubestApp.Common;
    using ClubestApp.Data.Models;
    using ClubestApp.Models.InputModels;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using System.Threading.Tasks;

    public class UserController : Controller
    {
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private readonly ILogger<RegisterModel> _logger;

        public UserController(
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            ILogger<RegisterModel> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
        }

        public IActionResult Index()
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