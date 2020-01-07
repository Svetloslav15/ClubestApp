namespace ClubestApp.Controllers
{
    using ClubestApp.Models.InputModels;
    using ClubestApp.Services;
    using Microsoft.AspNetCore.Mvc;

    public class ClubController : Controller
    {
        private ClubService clubService;

        public ClubController(ClubService clubService)
        {
            this.clubService = clubService;
        }

        public IActionResult AddClub()
        {
            return this.View();
        }

        [HttpPost]
        public IActionResult AddClub(AddClubInputModel model)
        {
            if (ModelState.IsValid)
            {
                this.clubService.AddClub(model);
            }

            return this.Redirect("/Home/Index");
        }
    }
}