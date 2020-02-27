namespace ClubestApp.Models.BindingModels.Events
{
    using ClubestApp.Data.Models;

    public class EventDetailsBindingModels
    {
        public Club Club { get; set; }

        public User CurrentUser { get; set; }

        public string ClubPriceType { get; set; }

        public Event Event { get; set; }
    }
}