namespace ClubestApp.Controllers
{
    using ClubestApp.Data.Models;
    using ClubestApp.Models.BindingModels.Events;
    using ClubestApp.Services;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Collections.Generic;
    using System.Security.Claims;
    using System.Threading.Tasks;

    public class EventController : Controller
    {
        private readonly ClubService clubService;
        private readonly EventService eventService;
        private readonly UserService userService;

        public EventController(ClubService clubService,
            EventService eventService, UserService userService)
        {
            this.clubService = clubService;
            this.eventService = eventService;
            this.userService = userService;
        }

        public async Task<IActionResult> Index(string id)
        {
            Club club = await this.clubService.GetClubById(id);
            IList<Event> events = await this.eventService.GetUpCommingEventsForClub(id);
            string currentUserId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            User currentUser = await this.userService.FindUserById(currentUserId);

            EventIndexBindingModel model = new EventIndexBindingModel()
            {
                Club = club,
                ClubPriceType = club.PriceType.ToString(),
                Events = events,
                CurrentUser = currentUser
            };
            return this.View(model);
        }

        public async Task<IActionResult> ExpiredEvents(string id)
        {
            Club club = await this.clubService.GetClubById(id);
            IList<Event> events = await this.eventService.GetUpExpiredEventsForClub(id);
            string currentUserId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            User currentUser = await this.userService.FindUserById(currentUserId);

            EventIndexBindingModel model = new EventIndexBindingModel()
            {
                Club = club,
                ClubPriceType = club.PriceType.ToString(),
                Events = events,
                CurrentUser = currentUser
            };
            return this.View("Index", model);
        }

        [HttpPost]
        public async Task<IActionResult> AddEvent(EventIndexBindingModel model)
        {
            int hours = model.AddEventInputModel.Date.Subtract(DateTime.UtcNow).Hours;
            if (ModelState.IsValid && model.AddEventInputModel.Date.Subtract(DateTime.UtcNow).Hours > 0)
            {
                await this.eventService.AddEvent(model.AddEventInputModel);
            }
            return this.Redirect($"/Event/Index/{model.AddEventInputModel.ClubId}");
        }

        public async Task<IActionResult> JoinEvent([FromQuery] string clubId, [FromQuery] string returnUrl, string id)
        {
            string currentUserId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            await this.eventService.JoinEvent(id, currentUserId);

            return this.Redirect(returnUrl);
        }

        public async Task<IActionResult> ExitEvent([FromQuery] string returnUrl, string id)
        {
            string currentUserId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            await this.eventService.ExitEvent(id, currentUserId);

            return this.Redirect(returnUrl);
        }

        public async Task<IActionResult> Details([FromQuery] string clubId, string id)
        {
            Event eventEntity = await this.eventService.GetEventById(id);
            Club club = await this.clubService.GetClubById(clubId);
            string currentUserId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            User currentUser = await this.userService.FindUserById(currentUserId);
            List<User> allUsers = await this.userService.GetAllUsers();

            EventDetailsBindingModels model = new EventDetailsBindingModels()
            {
                Event = eventEntity,
                Club = club,
                ClubPriceType = club.PriceType.ToString(),
                CurrentUser = currentUser,
                AllUsers = allUsers
            };

            return this.View(model);
        }

        public async Task<IActionResult> Delete([FromQuery] string clubId, string id)
        {
            await this.eventService.DeleteEvent(id);

            return this.Redirect($"/Event/Index/{clubId}");
        }

        public async Task<IActionResult> AddEventRole()
    }
}