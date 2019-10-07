namespace Clubest.Data.Models
{
    using Clubest.Data.Data.Models;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Event
    {
        public Event()
        {
            this.Id = Guid.NewGuid().ToString();
            this.UsersEvents = new List<UsersEvents>();
            this.Interests = new List<string>();
        }

        [Key]
        public string Id { get; set; }

        public string Name { get; set; }

        public DateTime Date { get; set; }

        public string Description { get; set; }

        public string ApplicationUserId { get; set; }

        public ApplicationUser ApplicationUser { get; set; }

        public string ClubId { get; set; }

        public Club Club { get; set; }

        public ICollection<UsersEvents> UsersEvents { get; set; }

        public bool IsPublic { get; set; }

        public ICollection<string> Interests { get; set; }
    }
}