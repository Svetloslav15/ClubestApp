namespace ClubestApp.Controllers
{
    using System.Linq;
    using Microsoft.AspNetCore.Mvc;
    using ClubestApp.Data.Models;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Authorization; 
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using ClubestApp.Models.BindingModels.Home;
    using ClubestApp.Common;
    using ClubestApp.Services;

    public class HomeController : Controller
    {
        private readonly UserManager<User> userManager;
        private readonly ClubService clubService;
        private readonly UserService userService;
        private readonly PostService postService;

        public HomeController(UserManager<User> userManager,
            ClubService clubService,
            UserService userService, 
            PostService postService)
        {
            this.userManager = userManager;
            this.userService = userService;
            this.clubService = clubService;
            this.postService = postService;
        }

        public async Task<IActionResult> Index([FromQuery(Name = "jcr")] bool hasJoinedClub)
        {
            var user = await this.userManager.GetUserAsync(HttpContext.User);

            if (user == null)
            {
                return this.View("IndexNotLogged");
            }
            if (user.Interests == null || user.Interests == "")
            {
                var interests = this.clubService.GetInterests();            
                return this.View("AddInterests", interests);
            }
            user = await this.userService.FindUserById(user.Id);

            if (user.UserClubs.Count() == 0)
            {
                List<Club> clubs = this.clubService.GetPotentialClubs(user.Interests, user.Town);
                return this.View("PotentialClubs", clubs);
            }
            IList<Post> posts = await this.postService.GetPostsForHomePage(user.Id);
            IndexPageBindingModel model = new IndexPageBindingModel()
            {
                Posts = posts
            };

            return this.View(model);
        }

        [Route("{*url}", Order = 999)]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            this.Response.StatusCode = 404;
            return this.View();
        }

        [Authorize(Roles = UserRoles.SystemAdmin)]
        public IActionResult Administration()
        {
            return this.Redirect("/Club/GetAllRequestNewClub");
        }
    }
}