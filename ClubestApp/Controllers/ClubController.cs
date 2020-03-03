namespace ClubestApp.Controllers
{
    using ClubestApp.Common;
    using ClubestApp.Data.Models;
    using ClubestApp.Models.BindingModels;
    using ClubestApp.Models.BindingModels.RequestNewClub;
    using ClubestApp.Models.InputModels;
    using ClubestApp.Services;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;

    public class ClubController : Controller
    {
        private readonly ClubService clubService;
        private readonly UserManager<User> userManager;
        private readonly RequestService requestService;
        private readonly PostService postService;
        private readonly PollService pollService;
        private readonly UserService userService;

        public ClubController(ClubService clubService,
                    UserManager<User> userManager,
                    RequestService requestService,
                    PostService postService,
                    PollService pollService,
                    UserService userService)
        {
            this.clubService = clubService;
            this.userManager = userManager;
            this.requestService = requestService;
            this.postService = postService;
            this.pollService = pollService;
            this.userService = userService;
        }
        

        [Authorize(Roles = "SystemAdmin")]
        public async Task<IActionResult> AddClub()
        {
            AddClubInputModel model = new AddClubInputModel();
            var interests = this.clubService.GetInterests();
            model.InterestsToList = interests;
            return this.View(model);
        }

        [Authorize(Roles = "SystemAdmin")]
        public async Task<IActionResult> Delete(string id)
        {
            await this.clubService.DeleteClubById(id);
            return this.Redirect("/Club/AllClubs");
        }

        [HttpPost]
        [Authorize(Roles = "SystemAdmin")]
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

        [Authorize]
        public IActionResult AddRequestNewClub()
        {
            AddClubInputModel model = new AddClubInputModel();
            var interests = this.clubService.GetInterests();
            model.InterestsToList = interests;
            return this.View(model);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddRequestNewClub(AddClubInputModel model)
        {
            if (ModelState.IsValid)
            {
                string userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
                await this.clubService.AddRequestNewClub(model, userId);

                return this.Redirect("/Home/Index");
            }

            var interests = this.clubService.GetInterests();
            model.InterestsToList = interests;
            return this.View(model);
        }
       
        [Authorize(Roles = "SystemAdmin")]
        public async Task<IActionResult> GetAllRequestNewClub()
        {
            List<RequestNewClub> requests = await this.clubService.GetRequestsNewClubAsync();
            ListAllRequestsNewClubBindingModel model = new ListAllRequestsNewClubBindingModel()
            {
                RequestsNewClub = requests
            };
            return this.View("ListAllRequestsNewClub", model);
        }

        [HttpGet]
        [Authorize]
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
        [Authorize]
        public async Task<IActionResult> JoinClub(string id)
        {
            User user = await this.userManager.GetUserAsync(HttpContext.User);
            await this.requestService.CreateJoinRequestClub(id, user);

            return this.Redirect($"/Club/Details/{id}");
        }

        [HttpGet]
        [Authorize]
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
            IList<Post> postsForClub = await this.postService.GetAllPostsForClub(club.Id);
            IList<Message> messages = await this.clubService.GetAllMessagesForClub(club.Id);
            ClubDetailsBindingModel bindingModel = new ClubDetailsBindingModel()
            {
                Club = club,
                ClubPriceType = clubPriceType,
                Posts = postsForClub,
                Messages = messages
            };

            return this.View(bindingModel);
        }

        [Authorize(Roles = "SystemAdmin, ClubAdmin")]
        public async Task<IActionResult> JoinRequests(string id)
        {
            Club club = await this.clubService.GetClubById(id);
            string clubPriceType = club.PriceType.ToString();
            List<JoinClubRequest> requests = this.requestService
                .GetPendingJoinClubRequestsForAClub(id)
                .GetAwaiter()
                .GetResult()
                .ToList();


            IList<Message> messages = await this.clubService.GetAllMessagesForClub(club.Id);
            ClubDetailsRequestsBindingModel requestsBindingModel = new ClubDetailsRequestsBindingModel()
            {
                Club = club,
                ClubPriceType = clubPriceType,
                JoinClubRequests = requests,
                Messages = messages
            };

            return this.View(requestsBindingModel);
        }

        [Authorize(Roles = "SystemAdmin, ClubAdmin")]
        public async Task<IActionResult> ArchivedJoinRequests(string id)
        {
            Club club = await this.clubService.GetClubById(id);
            string clubPriceType = club.PriceType.ToString();
            List<JoinClubRequest> requests = this.requestService
                .GetArchivedAndApprovedJoinClubRequestsForAClub(id)
                .GetAwaiter()
                .GetResult()
                .ToList();
            IList<Message> messages = await this.clubService.GetAllMessagesForClub(club.Id);

            ClubDetailsRequestsBindingModel requestsBindingModel = new ClubDetailsRequestsBindingModel()
            {
                Club = club,
                ClubPriceType = clubPriceType,
                JoinClubRequests = requests,
                Messages = messages
            };

            return this.View(requestsBindingModel);
        }

        [Authorize]
        public async Task<IActionResult> Polls([FromQuery] string validation, string id)
        {
            string userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            ListPollsBindingModel model = await this.pollService.GetPollsModel(id, userId);
            ViewData["validation"] = validation;

            return this.View(model);
        }

        [HttpPost]
        [Authorize(Roles = "SystemAdmin, ClubAdmin")]
        public async Task<IActionResult> ApproveJoinRequestClub(ClubDetailsRequestsBindingModel model)
        {
            await this.requestService.ApproveJoinClubRequest(model.RequestApproveBindingModel.RequestId, model.RequestApproveBindingModel.RequestType);
            return this.Redirect($"/Club/JoinRequests/{model.RequestApproveBindingModel.ClubId}");
        }

        [HttpPost]
        [Authorize(Roles = "SystemAdmin, ClubAdmin")]
        public async Task<IActionResult> DeleteJoinRequestClub(ClubDetailsRequestsBindingModel model)
        {
            await this.requestService.DeleteJoinClubRequest(model.RequestApproveBindingModel.RequestId);
            return this.Redirect($"/Club/ArchivedJoinRequests/{model.RequestApproveBindingModel.ClubId}");
        }

        public async Task<IActionResult> ListMembers(string id)
        {
            ListClubMembersBindingModel model = await this.clubService.GetMemberInClub(id);
            return View(model);
        }

        [Authorize(Roles = "SystemAdmin, ClubAdmin")]
        public async Task<IActionResult> Edit(string id)
        {
            EditClubBindingModel model = await this.clubService.GetEditClubModel(id);

            return this.View(model);
        }

        [HttpPost]
        [Authorize(Roles = "SystemAdmin, ClubAdmin")]
        public async Task<IActionResult> Edit(EditClubInputModel model, string id)
        {
            bool isFileValid = true;
            if (model.ImageFile != null)
            {
                isFileValid = this.clubService.IsFileValid(model.ImageFile);
            }

            if (ModelState.IsValid && isFileValid && model.Interests.Any() == true)
            {
                string userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
                await this.clubService.EditClub(model, id);

                return this.Redirect("/Club/Details/" + id);
            }

            var interests = this.clubService.GetInterests();
            model.InterestsToList = interests;

            if (model.Interests.Any() == false)
            {
                ModelState.AddModelError(ClubFields.Interests, ErrorMessages.ClubInterestsRequired);
            }
            var bindingModel = new EditClubBindingModel()
            {
                Id = id,
                Name = model.Name,
                Fee = model.Fee,
                IsPublic = model.IsPublic,
                PriceType = model.PriceType,
                Description = model.Description,
                Town = model.Town,
                PictureUrl = model.PictureUrl,
                ImageFile = model.ImageFile,
                InterestsToList = interests
            };
            return this.View(bindingModel);
        }

        [Authorize(Roles = "SystemAdmin, ClubAdmin")]
        public async Task<IActionResult> PollsAdministration(string id)
        {
            AdministrationPollsBindingModel model = await this.pollService.GetAdministrationBindingModel(id);

            return this.View(model);
        }

        [Authorize(Roles = "SystemAdmin, ClubAdmin")]
        public async Task<IActionResult> AddClubAdmin(string id, string secondId)
        {
            Club club = await this.clubService.GetClubById(id);
            User user = await this.userService.FindUserById(secondId);
            await this.clubService.AddClubAdmin(club, user);

            return this.Redirect($"/Club/ListMembers/{club.Id}");
        }

        [Authorize(Roles = "SystemAdmin, ClubAdmin")]
        public async Task<IActionResult> RemoveClubAdmin(string id, string secondId)
        {
            Club club = await this.clubService.GetClubById(id);
            User user = await this.userService.FindUserById(secondId);
            await this.clubService.RemoveClubAdmin(club, user);

            return this.Redirect($"/Club/ListMembers/{club.Id}");
        }
    }
}