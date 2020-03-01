namespace ClubestApp.Services
{
    using ClubestApp.Data;
    using ClubestApp.Data.Models;
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Linq;
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

        public async Task<IList<Notification>> GetNotificationsForUser(string userId)
        {
            return await this.dbContext.Notifications
                .Where(x => x.UserId == userId)
                .ToListAsync();
        }

        public async Task<IList<Notification>> GetUnReadNotificationsForUser(string userId)
        {
            return await this.dbContext.Notifications
                .Where(x => x.UserId == userId && x.IsRead == false)
                .ToListAsync();
        }

        public async Task<IList<Notification>> ReadAllNotificationsForUser(string userId)
        {
            IList<Notification> notifications = await this.GetNotificationsForUser(userId);
            foreach (Notification notification in notifications)
            {
                await this.ReadNotification(notification.Id);
            }

            return notifications;
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
            await this.dbContext.Notifications.AddAsync(notification);
            await this.dbContext.SaveChangesAsync();
            return notification;
        }

        public async Task<Notification> CreateNotificationForListOfUsers(string content, string link, IList<UserClub> users)
        {
            Notification notification = null;
            foreach (UserClub userClub in users)
            {
                notification = await this.CreateNotification(content, link, userClub.UserId);
            }

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