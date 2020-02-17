namespace ClubestApp.Controllers
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
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;

    public class UserController : Controller
    {
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly UserService userService;
        private const string defaultPictureUrl = @"https://res.cloudinary.com/dzivpr6fj/image/upload/v1580902697/ClubestPics/24029_llq8xg.png";

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

        public async Task<IActionResult> Profile()
        {
            //Finds user by his id
            string id = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            User user = await this.userService.FindUserById(id);

            EditProfileBindingModel model = new EditProfileBindingModel()
            {
                Email = user.Email,
                Username = user.UserName,
                FirstName = user.FirstName,
                LastName = user.LastName,
                PhoneNumber = user.PhoneNumber,
                PictureUrl = user.PictureUrl != null ? user.PictureUrl
                                : defaultPictureUrl
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Profile(EditProfileInputModel model)
        {
            if (ModelState.IsValid)
            {
                await this.userService.EditUser(model);
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
                if (changePasswordResult.Succeeded)
                {
                    await _signInManager.RefreshSignInAsync(user);
                    ViewData["Message"] = "Успешно сменихте паролата си!";
                }
                else
                {
                    ViewData["ЕrrorMessage"] = "Невалидни данни!";
                }
            }
            return this.View("ChangePassword");
        }

        //TODO
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
                    Town = inputModel.Town
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

        public async Task<IActionResult> Logout()
        {
            await this._signInManager.SignOutAsync();
            return this.Redirect("/");
        }

        [HttpPost]
        public async Task<IActionResult> AddInterestsToUser(AddInterestsInputModel inputModel)
        {
            if (ModelState.IsValid)
            {
                User user = await _userManager.GetUserAsync(User);
                await this.userService.AddInterestsToUser(inputModel, user.Id);
            }

            return this.Redirect("/");
        }


        public async Task<IActionResult> Interests()
        {
            User user = await _userManager.GetUserAsync(User);
            EditInterestsBindingModel model = new EditInterestsBindingModel
            {
                AllInterests = await this.userService.GetInterests(),
                UserInterests = user.Interests.Split(", ", System.StringSplitOptions.RemoveEmptyEntries).ToList()
            };

            return this.View(model);
        }


        [HttpPost]
        public async Task<IActionResult> Interests(AddInterestsInputModel inputModel)
        {

            //Removing all interests and adding new one(in case of deleting old interests)
            if (inputModel.Interests == null)
            {
                User user = await _userManager.GetUserAsync(User);
                user.Interests = "";
                this.userService.RemoveUserAllInterests(user);
            }
            else
            {
                User user = await _userManager.GetUserAsync(User);
                user.Interests = "";
                await this.userService.AddInterestsToUser(inputModel, user.Id);
            }

            return this.Redirect("Interests");
        }

        [HttpPost]
        public async Task<IActionResult> ChangePhoto(EditProfileInputModel inputModel)
        {
            if (inputModel.PhotoFile == null)
            {
                return this.Redirect("/User/Profile");
            }

            bool isFileValid = this.userService.IsFileValid(inputModel.PhotoFile);

            if (isFileValid == false)
            {
                return this.RedirectToAction("Profile");
            }

            User user = await this._userManager.GetUserAsync(User);

            //Change only user's photo without checking other data
            await this.userService.ChangeProfilePicture(user, inputModel.PhotoFile);

            return this.Redirect("/User/Profile");
        }
    }
}