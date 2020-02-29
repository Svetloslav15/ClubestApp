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

        public async Task SendMessage(string message, string clubId)
        {
            var allUsers = await this.userService.GetAllUsers();
            string userId = allUsers
                .First(x => x.UserName == this.Context.User.Identity.Name)
                .Id;
            string connectionId = Context.ConnectionId;
            User user = await this.userService.FindUserById(userId);
            string pictureUrl = user.PictureUrl;
<<<<<<< HEAD

            string connectionId = Context.ConnectionId;
            await this.Clients.AllExcept(connectionId)
                .SendAsync("ReceiveMessage", pictureUrl, message, clubId);
        }

        public async Task Join()
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, "foo"); 
=======
            await this.Clients.AllExcept(connectionId)
                .SendAsync("ReceiveMessage", pictureUrl, message);
>>>>>>> 3a569276e83f3b9fe164bc2c724572bbe22c6227
        }
    }
}