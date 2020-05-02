namespace ClubestApp.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Poll
    {
        public Poll()
        {
            this.Id = Guid.NewGuid().ToString();
            this.PollVotedUsers = new HashSet<PollVotedUsers>();
            this.Options = new HashSet<Option>();
        }

        [Key]
        public string Id { get; set; }

        public string Content { get; set; }

        public string ClubId { get; set; }

        public Club Club { get; set; }

        public bool IsMultichoice { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime ExpiredDate { get; set; }

        public ICollection<PollVotedUsers> PollVotedUsers { get; set; }

        public ICollection<Option> Options { get; set; }
    }
}