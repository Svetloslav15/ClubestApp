namespace ClubestApp.Controllers
{
    using ClubestApp.Data.Models;
    using ClubestApp.Models.BindingModels;
    using ClubestApp.Models.InputModels;
    using ClubestApp.Services;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;

    public class ClubController : Controller
    {
        private readonly ClubService clubService;
        private readonly UserManager<User> userManager;
        private readonly RequestService requestService;

        public ClubController(ClubService clubService,
                    UserManager<User> userManager,
                    RequestService requestService)
        {
            this.clubService = clubService;
            this.userManager = userManager;
            this.requestService = requestService;
        }

        public IActionResult AddClub()
        {
            AddClubInputModel model = new AddClubInputModel();
            var interests = this.clubService.GetInterests();
            model.InterestsToList = interests;
            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddClub(AddClubInputModel model)
        {
            if (ModelState.IsValid)
            {
                string userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
                await this.clubService.AddClub(model, userId);

                return this.Redirect("/Home/Index");
            }

            var interests = this.clubService.GetInterests();
            model.InterestsToList = interests;
            return this.View(model);
        }

        [HttpGet]
        public async Task<IActionResult> AllClubs(IList<Club> clubs)
        {
            if (clubs.Count == 0)
            {
                clubs = await this.clubService.GetAllClubs();
            }
            GetClubsBindingModel[] model = this.clubService.GetAllClubsBindingModel(clubs);

            return this.View(model);
        }

        [HttpGet]
        public async Task<IActionResult> JoinClub(string id)
        {
            User user = await this.userManager.GetUserAsync(HttpContext.User);
            await this.requestService.CreateJoinRequestClub(id, user);
            
            return this.Redirect("/?jcr=true");
        }

        [HttpGet]
        public async Task<IActionResult> SearchClub([FromQuery(Name = "club")] string userInput)
        {
            IList<Club> clubs = await this.clubService.GetAllClubs();
            
            if (userInput != null && userInput != "")
            {
                clubs = await this.clubService.FilterClubsBySearchInput(userInput);
            }
            
            GetClubsBindingModel[] model = this.clubService.GetAllClubsBindingModel(clubs);
            return this.View("AllClubs", model);
        }

        [HttpGet]
        public async Task<IActionResult> Details(string id)
        {
            Club club = await this.clubService.GetClubById(id);
            string clubPriceType = club.PriceType.ToString();
            ClubDetailsBindingModel bindingModel = new ClubDetailsBindingModel()
            {
                Club = club,
                ClubPriceType = clubPriceType
            };

            return this.View(bindingModel);
        }

        [Authorize]
        public async Task<IActionResult> JoinRequests(string id)
        {
            Club club = await this.clubService.GetClubById(id);
            string clubPriceType = club.PriceType.ToString();
            List<JoinClubRequest> requests = this.requestService.GetJoinClubRequestsForAClub(id)
                .GetAwaiter()
                .GetResult()
                .ToList();

            ClubDetailsRequestsBindingModel requestsBindingModel = new ClubDetailsRequestsBindingModel()
            {
                Club = club,
                ClubPriceType = clubPriceType,
                JoinClubRequests = requests
            };

            return this.View(requestsBindingModel);
        }

        [HttpPost]
        public async Task<IActionResult> ApproveJoinRequestClub(ClubDetailsRequestsBindingModel model)
        {
            await this.requestService.ApproveJoinClubRequest(model.RequestApproveBindingModel.RequestId, model.RequestApproveBindingModel.RequestType);
            return this.Redirect($"/Club/JoinRequests/{model.RequestApproveBindingModel.ClubId}");
        }

        public async Task<IActionResult> ListMembers(string id)
        {
            ListClubMemebersBindingModel model = await this.clubService.GetMemberInClub(id);
            return View(model);
        }

        public async Task<IActionResult> Edit(string id)
        {
            EditClubBindingModel model = await this.clubService.GetEditClubModel(id);

            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditClubInputModel model, string id)
        {
            bool isFileValid = true;
            if (model.ImageFile != null)
            {
                isFileValid = this.clubService.IsFileValid(model.ImageFile);
            }

            if (ModelState.IsValid || isFileValid)
            {
                string userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
                await this.clubService.EditClub(model, id);

                return this.Redirect("/Club/Details/" + id);
            }

            var interests = this.clubService.GetInterests();
            model.InterestsToList = interests;

            return this.View(model);
        }
    }
}