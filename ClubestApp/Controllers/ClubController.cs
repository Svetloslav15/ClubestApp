namespace ClubestApp.Controllers
{
    using ClubestApp.Data.Models;
    using ClubestApp.Models.BindingModels;
    using ClubestApp.Models.InputModels;
    using ClubestApp.Services;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using System.Security.Claims;
    using System.Threading.Tasks;

    public class ClubController : Controller
    {
        private readonly ClubService clubService;
        private readonly UserManager<User> userManager;

        public ClubController(ClubService clubService,
               UserManager<User> userManager)
        {
            this.clubService = clubService;
            this.userManager = userManager;
        }

        public IActionResult AddClub()
        {
            AddClubInputModel model = new AddClubInputModel();
            var interests = this.clubService.GetInterests();
            model.InterestsToList = interests;
            return this.View(model);
        }

        [HttpPost]
        public IActionResult AddClub(AddClubInputModel model)
        {
            if (ModelState.IsValid)
            {
                string userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
                this.clubService.AddClub(model, userId);

                return this.Redirect("/Home/Index");
            }
            var interests = this.clubService.GetInterests();
            model.InterestsToList = interests;
            return this.View(model);
        }

        [HttpGet]
        public IActionResult AllClubs()
        {
            //Get all clubs from db
            GetClubsBindingModel[] model = this.clubService.GetAllClubsBindingModel();

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> JoinClub(string id)
        {
            User user = await this.userManager.GetUserAsync(HttpContext.User);
            JoinClubRequest request = this.clubService.CreateJoinRequestClub(id, user);
            
            return this.Redirect("/?jcr=true");
        }
    }
}