namespace ClubestApp.Data.Models
{
    public class EventUser
    {
        public string UserId { get; set; }

        public User User { get; set; }

        public string EventId { get; set; }

        public Event Event { get; set; }

        public string Role { get; set; }
    }
}