namespace ClubestApp.Controllers
{
    using ClubestApp.Data.Models;
    using ClubestApp.Models.BindingModels.Events;
    using ClubestApp.Models.InputModels.Events;
    using ClubestApp.Services;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;

    public class EventController : Controller
    {
        private readonly ClubService clubService;
        private readonly EventService eventService;

        public EventController(ClubService clubService,
            EventService eventService)
        {
            this.clubService = clubService;
            this.eventService = eventService;
        }

        public async Task<IActionResult> Index(string id)
        {
            Club club = await this.clubService.GetClubById(id);
            EventIndexBindingModel model = new EventIndexBindingModel()
            {
                Club = club,
                ClubPriceType = club.PriceType.ToString()
            };
            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddEvent(EventIndexBindingModel model)
        {
            if (ModelState.IsValid)
            {
                await this.eventService.AddEvent(model.AddEventInputModel);
            }
            return this.Redirect($"/Event/Index/{model.AddEventInputModel.ClubId}");
        }
    }
}