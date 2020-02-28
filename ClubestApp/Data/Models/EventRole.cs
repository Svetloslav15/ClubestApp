namespace ClubestApp.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class EventRole
    {
        public EventRole()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        [Key]
        public string Id { get; set; }

        public string Name { get; set; }

        public string EventId { get; set; }

        public Event Event { get; set; }

        public string UserId { get; set; }

        public User User { get; set; }
    }
}
