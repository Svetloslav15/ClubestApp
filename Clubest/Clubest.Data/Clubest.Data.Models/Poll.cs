namespace Clubest.Data.Models
{
    using Clubest.Data.Data.Models;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Poll
    {
        public Poll()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Options = new List<Option>();
            this.UsersPolls = new List<UsersPolls>();
            this.Interests = new List<string>();
            this.VotedUsers = new List<string>();
        }

        [Key]
        public string Id { get; set; }

        public string Title { get; set; }

        public ICollection<Option> Options { get; set; }

        public ICollection<UsersPolls> UsersPolls { get; set; }

        [NotMapped]
        public ICollection<string> Interests { get; set; }

        public string ClubId { get; set; }

        public Club Club { get; set; }

        public DateTime ExpiredDate { get; set; }

        [NotMapped]
        public ICollection<string> VotedUsers { get; set; }
    }
}