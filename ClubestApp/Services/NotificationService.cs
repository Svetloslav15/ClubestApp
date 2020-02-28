namespace ClubestApp.Services
{
    using ClubestApp.Data;
    using ClubestApp.Data.Models;
    using Microsoft.EntityFrameworkCore;
    using System.Threading.Tasks;

    public class NotificationService
    {
        private readonly ApplicationDbContext dbContext;
        private readonly UserService userService;

        public NotificationService(ApplicationDbContext dbContext,
            UserService userService)
        {
            this.dbContext = dbContext;
            this.userService = userService;
        }

        public async Task<Notification> GetNotificationById(string id)
        {
            return await this.dbContext.Notifications
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Notification> CreateNotification(string content, string link, string userId)
        {
            User user = await this.userService.FindUserById(userId);
            Notification notification = new Notification()
            {
                Content = content,
                Link = link,
                UserId = userId,
                User = user,
                IsRead = false
            };

            return notification;
        }

        public async Task<Notification> ReadNotification(string id)
        {
            Notification notification = await this.GetNotificationById(id);
            notification.IsRead = true;

            await this.dbContext.SaveChangesAsync();
            return notification;
        }
    }
}