namespace ClubestApp.Controllers
{
    using ClubestApp.Data.Models;
    using ClubestApp.Models.BindingModels;
    using ClubestApp.Models.InputModels;
    using ClubestApp.Services;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;
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
        public IActionResult AllClubs(IList<Club> clubs)
        {
            if (clubs.Count == 0)
            {
                clubs = this.clubService.GetAllClubs();
            }
            GetClubsBindingModel[] model = this.clubService.GetAllClubsBindingModel(clubs);

            return this.View(model);
        }

        [HttpGet]
        public async Task<IActionResult> JoinClub(string id)
        {
            User user = await this.userManager.GetUserAsync(HttpContext.User);
            JoinClubRequest request = this.clubService.CreateJoinRequestClub(id, user);
            
            return this.Redirect("/?jcr=true");
        }

        [HttpGet]
        public IActionResult SearchClub([FromQuery(Name = "club")] string userInput)
        {
            IList<Club> clubs = this.clubService.GetAllClubs();
            
            if (userInput != null && userInput != "")
            {
                clubs = this.clubService.FilterClubsBySearchInput(userInput);
            }
            
            GetClubsBindingModel[] model = this.clubService.GetAllClubsBindingModel(clubs);
            return this.View("AllClubs", model);
        }

        [HttpGet]
        public IActionResult Details(string id)
        {
            Club club = this.clubService.GetClubById(id);
            ClubDetailsBindingModel bindingModel = new ClubDetailsBindingModel()
            {
                Club = club
            };

            return this.View(bindingModel);
        }    
    }
}