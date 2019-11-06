namespace ClubestApp.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Event
    {
        public Event()
        {
            this.Id = Guid.NewGuid().ToString();
            this.EventUsers = new HashSet<EventUser>();
        }

        [Key]
        public string Id { get; set; }

        public string Name { get; set; }

        public DateTime Date { get; set; }

        public ICollection<EventUser> EventUsers { get; set; }

        public string AdminId { get; set; }

        public User Admin { get; set; }

        public string Description { get; set; }

        public string ClubId { get; set; }

        public Club Club { get; set; }

        public bool IsPublic { get; set; }

        public string Interests { get; set; }
    }
}