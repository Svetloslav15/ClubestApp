namespace ClubestApp.Models.BindingModels.Events
{
    using ClubestApp.Data.Models;
    using ClubestApp.Models.InputModels.Events;
    using System.Collections.Generic;

    public class EventIndexBindingModel
    {
        public Club Club { get; set; }

        public User CurrentUser { get; set; }

        public string ClubPriceType { get; set; }

        public List<User> UsersForSelectAdmin { get; set; }

        public AddEventInputModel AddEventInputModel { get; set; }

        public IList<Event> Events { get; set; }
    }
}