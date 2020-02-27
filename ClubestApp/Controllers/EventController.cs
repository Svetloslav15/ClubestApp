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
    }
}