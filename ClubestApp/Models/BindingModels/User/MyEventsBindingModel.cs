namespace ClubestApp.Models.BindingModels.User
{
    using ClubestApp.Data.Models;
    using System.Collections.Generic;

    public class MyEventsBindingModel
    {
        public IList<EventUser> EventUsers { get; set; } = new List<EventUser>();
    }
}