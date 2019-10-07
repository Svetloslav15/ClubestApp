namespace Clubest.Data.Data.Models
{
    using Clubest.Data.Models;
    using Microsoft.AspNetCore.Identity;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class ApplicationUser: IdentityUser<string>
    {
        public ApplicationUser()
        {
            this.Interests = new HashSet<string>();
            this.UsersPolls = new List<UsersPolls>();
            this.Posts = new List<Post>();
            this.Notifications = new List<Notification>();
            this.UsersClubs = new List<UsersClubs>();
            this.UsersEvents = new List<UsersEvents>();
            this.Comments = new List<Comment>();
            this.RequestNewClubs = new List<RequestNewClub>();
        }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public ICollection<string> Interests { get; set; }

        public ICollection<Post> Posts { get; set; }

        public ICollection<Notification> Notifications { get; set; }

        public ICollection<UsersClubs> UsersClubs { get; set; }

        public ICollection<UsersPolls> UsersPolls { get; set; }

        public ICollection<UsersEvents> UsersEvents { get; set; }

        public ICollection<Comment> Comments { get; set; }

        public ICollection<RequestNewClub> RequestNewClubs { get; set; }
    }
}