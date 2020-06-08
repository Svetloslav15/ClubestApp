namespace ClubestApp.Controllers
{
    using ClubestApp.Areas.Identity.Pages.Account;
    using ClubestApp.Common;
    using ClubestApp.Data.Models;
    using ClubestApp.Models.BindingModels;
    using ClubestApp.Models.BindingModels.User;
    using ClubestApp.Models.InputModels;
    using ClubestApp.Models.InputModels.Users;
    using ClubestApp.Services;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;

    public class UserController : Controller
    {
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly UserService userService;
        private readonly EventService eventService;
        private readonly PasswordTokenService passwordTokenService;
        private const string defaultPictureUrl = @"https://res.cloudinary.com/dp1c8zoit/image/upload/v1586440816/ClubestPics/24029_llq8xg.png";

        public UserController(
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            ILogger<RegisterModel> logger,
            UserService userService,
            EventService eventService,
            PasswordTokenService passwordTokenService)
        {
            this._userManager = userManager;
            this._signInManager = signInManager;
            this._logger = logger;
            this.userService = userService;
            this.eventService = eventService;
            this.passwordTokenService = passwordTokenService;
        }

        [Authorize]
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
                PictureUrl = user.PictureUrl
            };

            return View(model);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Profile(EditProfileInputModel model)
        {
            if (ModelState.IsValid)
            {
                await this.userService.EditUser(model);
            }

            return this.Redirect("/User/Profile");
        }

        [Authorize]
        public IActionResult ChangePassword()
        {
            return this.View();
        }

        [Authorize]
        public async Task<IActionResult> Events()
        {
            User user = await _userManager.GetUserAsync(User);
            IList<EventUser> eventUsers = await this.eventService.GetEventUsersForUser(user.Id);
            
            MyEventsBindingModel model = new MyEventsBindingModel()
            {
                EventUsers = eventUsers
            };

            return this.View("MyEvents", model);
        }

        [HttpPost]
        [Authorize]
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

        public async Task<IActionResult> ChangeForgottenPassword([FromQuery] string id)
        {
            ViewData["TokenId"] = id;

            return this.View("ForgottenPassChangePassword");
        }

        [HttpPost]
        public async Task<IActionResult> ChangeForgottenPassword(NewPasswordInputModel inputModel)
        {
            if (ModelState.IsValid)
            {
                PasswordToken token = await this.passwordTokenService.FindById(inputModel.PasswordTokenId);

                if (inputModel.Password == inputModel.ConfirmPassword)
                {
                    string resetToken = await this._userManager.GeneratePasswordResetTokenAsync(token.User);
                    await this._userManager.ResetPasswordAsync(token.User, resetToken, inputModel.Password);

                    return this.Redirect("/");
                }

                return this.Redirect($"/ChangeForgottenPassword?id={inputModel.PasswordTokenId}");
            }

            return this.Redirect("/");
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
                    Town = inputModel.Town,
                    PictureUrl = defaultPictureUrl
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
        [Authorize]
        public async Task<IActionResult> AddInterestsToUser(AddInterestsInputModel inputModel)
        {
            if (ModelState.IsValid)
            {
                User user = await _userManager.GetUserAsync(User);
                await this.userService.AddInterestsToUser(inputModel, user.Id);
            }

            return this.Redirect("/");
        }

        [Authorize]
        public async Task<IActionResult> Interests()
        {
            User user = await _userManager.GetUserAsync(User);
            EditInterestsBindingModel model = new EditInterestsBindingModel
            {
                AllInterests = await this.userService.GetInterests(),
                UserInterests = user.Interests.Split(",", System.StringSplitOptions.RemoveEmptyEntries).ToList()
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
        [Authorize]
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

        [Authorize]
        public async Task<IActionResult> MyClubs()
        {
            string userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            MyClubsViewModel model = await this.userService.GetUsersClubs(userId);

            return View(model);
        }

        public async Task<IActionResult> ForgottenPassword()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> ForgottenPassword(ForgottenPasswordInputModel inputModel)
        {
            string redirectUrl = "/login";
            if (ModelState.IsValid)
            {
                User user = await this.userService.FindUserByEmail(inputModel.Email);
                if (user != null)
                {
                    PasswordToken token = await this.passwordTokenService.GeneratePasswordToken(user);
                    await this.userService.SendMailToUserForForgottenPassword(inputModel.Email, token);
                    return this.Redirect("/");
                }

                redirectUrl = "/login";
            }

            return this.Redirect(redirectUrl);
        }
    }
}