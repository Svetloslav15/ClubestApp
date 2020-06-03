namespace ClubestApp.Services.Contracts
{
    using ClubestApp.Data.Models;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface INotificationService
    {
        Task<Notification> GetNotificationById(string id);

        Task<IList<Notification>> GetNotificationsForUser(string userId);

        Task<IList<Notification>> GetUnReadNotificationsForUser(string userId);
        
        Task<IList<Notification>> ReadAllNotificationsForUser(string userId);

        Task<Notification> CreateNotification(string content, string link, string userId);

        Task<Notification> CreateNotificationForListOfUsers(string content, string link, IList<UserClub> users);

        Task<Notification> ReadNotification(string id);
    }
}