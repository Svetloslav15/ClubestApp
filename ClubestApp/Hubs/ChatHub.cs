namespace ClubestApp.Hubs
{
    using ClubestApp.Data.Models;
    using ClubestApp.Services;
    using Microsoft.AspNetCore.SignalR;
    using System.Threading.Tasks;
    using System.Linq;

    public class ChatHub : Hub
    {
        private readonly UserService userService;

        public ChatHub(UserService userService)
        {
            this.userService = userService;
        }

        public async Task SendMessage(string message)
        {
            var allUsers = await this.userService.GetAllUsers();
            string userId = allUsers
                .First(x => x.UserName == this.Context.User.Identity.Name)
                .Id;
            string connectionId = Context.ConnectionId;
            User user = await this.userService.FindUserById(userId);
            string pictureUrl = user.PictureUrl;
            await this.Clients.AllExcept(connectionId)
                .SendAsync("ReceiveMessage", pictureUrl, message);
        }
    }
}