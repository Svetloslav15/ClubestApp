namespace Clubest.Data.Data.Models
{
    using Clubest.Data.Models;
    using Clubest.Data.Models.Enums;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Club
    {
        public Club()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Posts = new List<Post>();
            this.Events = new List<Event>();
            this.Admins = new List<string>();
            this.UsersClubs = new List<UsersClubs>();
            this.Polls = new List<Poll>();
        }

        [Key]
        public string Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Tax { get; set; }

        public ICollection<UsersClubs> UsersClubs { get; set; }

        public ICollection<Post> Posts { get; set; }

        public ICollection<Event> Events { get; set; }

        [NotMapped]
        public ICollection<string> Admins { get; set; }

        public ICollection<Poll> Polls { get; set; }

        public PriceType PriceType { get; set; }

        public string ProfilePicture { get; set; }
    }
}