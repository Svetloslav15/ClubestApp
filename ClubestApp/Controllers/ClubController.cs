namespace ClubestApp.Controllers
{
    using ClubestApp.Models.BindingModels;
    using ClubestApp.Models.InputModels;
    using ClubestApp.Services;
    using Microsoft.AspNetCore.Mvc;
    using System.Security.Claims;

    public class ClubController : Controller
    {
        private readonly ClubService clubService;

        public ClubController(ClubService clubService)
        {
            this.clubService = clubService;
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
    }
}