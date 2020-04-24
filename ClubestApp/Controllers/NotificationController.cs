namespace ClubestApp.Controllers
{
    using ClubestApp.Data.Models;
    using ClubestApp.Models.BindingModels.Notifications;
    using ClubestApp.Services;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;
    using System.Security.Claims;
    using System.Threading.Tasks;

    public class NotificationController : Controller
    {
        private readonly NotificationService notificationService;

        public NotificationController(NotificationService notificationService)
        {
            this.notificationService = notificationService;
        }

        [Authorize]
        public async Task<IActionResult> Index()
        {
            string userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            IList<Notification> notifications = await this.notificationService.GetNotificationsForUser(userId);

            NotificationsIndexBindingModel model = new NotificationsIndexBindingModel()
            {
                Notifications = notifications
            };
            await this.notificationService.ReadAllNotificationsForUser(userId);
            return View(model);
        }
    }
}