namespace ClubestApp.Controllers
{
    using ClubestApp.Models.BindingModels;
    using ClubestApp.Models.InputModels;
    using ClubestApp.Services;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;

    public class PollController : Controller
    {
        private readonly PollService pollService;

        public PollController(PollService pollService)
        {
            this.pollService = pollService;
        }

        [HttpPost]
        public async Task<IActionResult> AddPoll(AddPollInputModel model)
        {
            if (ModelState.IsValid && model.ExpiredDate.Subtract(DateTime.UtcNow).Hours > 0)
            {
                await this.pollService.CreatePoll(model, model.ClubId);
                return this.Redirect($"/Club/Polls/{model.ClubId}");
            }

            return this.Redirect($"/Club/Polls/{model.ClubId}?validation=true");
        }

        [HttpPost]
        public async Task<IActionResult> AddVote(AddPollInputModel model)
        {
            string userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            if (model.Votes?.Any() == true)
            {
                await this.pollService.AddVote(model.Votes, model.PollId, userId);
                return this.Redirect($"/Club/Polls/{model.ClubId}");
            }

            return this.Redirect($"/Club/Polls/{model.ClubId}");
        }

        public async Task<IActionResult> Delete([FromQuery] string clubId, string id)
        {
            var deletedEntity = await this.pollService.DeletePoll(id);
            return this.Redirect($"/Club/PollsAdministration/{clubId}");
        }
    }
}
