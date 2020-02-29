namespace ClubestApp.Controllers
{
    using ClubestApp.Data.Models;
    using ClubestApp.Models.InputModels.Message;
    using ClubestApp.Services;
    using Microsoft.AspNetCore.Mvc;
    using System.Security.Claims;
    using System.Threading.Tasks;

    public class MessageController : Controller
    {
        private readonly MessageService messageService;
        private readonly UserService userService;

        public MessageController(MessageService messageService, UserService userService)
        {
            this.messageService = messageService;
            this.userService = userService;
        }

        public async Task<IActionResult> Add([FromQuery] MessageInputModel model)
        {
            string userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            User user = await this.userService.FindUserById(userId);

            Message message = await this.messageService.AddMessage(model.Content, model.ClubId, user);

            return this.Content(user.PictureUrl);
        }
    }
}
