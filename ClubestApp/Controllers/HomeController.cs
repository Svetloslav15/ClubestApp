namespace ClubestApp.Controllers
{
    using System.Diagnostics;
    using System.Linq;
    using Microsoft.AspNetCore.Mvc;
    using ClubestApp.Models;
    using ClubestApp.Data.Models;
    using Microsoft.AspNetCore.Identity;
    using System.Threading.Tasks;
    using ClubestApp.Services;
    using System.Collections.Generic;

    public class HomeController : Controller
    {
        private readonly UserManager<User> userManager;
        private readonly ClubService clubService;

        public HomeController(UserManager<User> userManager, ClubService clubService)
        {
            this.userManager = userManager;
            this.clubService = clubService;
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
            if (user.UserClubs.Count() == 0)
            {
                List<Club> clubs = this.clubService.GetPotentialClubs(user.Interests, user.Town);
                return this.View("PotentialClubs", clubs);
            }

            //TODO load latest data from clubs
            return this.View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}