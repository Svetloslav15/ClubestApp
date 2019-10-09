namespace Clubest.Data.Models
{
    using Clubest.Data.Data.Models;

    public class UsersEvents
    {
        public string UserId { get; set; }

        public ApplicationUser User { get; set; }

        public string EventId { get; set; }

        public Event Event { get; set; }
    }
}