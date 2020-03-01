namespace ClubestApp.Models.BindingModels.Notifications
{
    using ClubestApp.Data.Models;
    using System.Collections.Generic;

    public class NotificationsIndexBindingModel
    {
        public IList<Notification> Notifications { get; set; } = new List<Notification>();
    }
}