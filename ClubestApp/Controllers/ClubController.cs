namespace ClubestApp.Controllers
{
    using ClubestApp.Common;
    using ClubestApp.Models.InputModels;
    using ClubestApp.Services;
    using Microsoft.AspNetCore.Mvc;
    using System.Security.Claims;

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
                string userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
                this.clubService.AddClub(model, userId);

                return this.Redirect("/Home/Index");
            }


            return this.View();
        }
    }
}