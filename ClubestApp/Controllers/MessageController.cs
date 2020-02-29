namespace ClubestApp.Controllers
{
    using ClubestApp.Models.InputModels.Message;
    using ClubestApp.Services;
    using Microsoft.AspNetCore.Mvc;

    public class MessageController : Controller
    {
        private readonly MessageService messageService;

        public MessageController(MessageService messageService)
        {
            this.messageService = messageService;
        }

        [HttpPost]
        public IActionResult Add(string ClubId)
        {
            return this.Content(ClubId);
        }
    }
}
