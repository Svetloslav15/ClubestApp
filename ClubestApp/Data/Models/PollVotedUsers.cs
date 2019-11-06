using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClubestApp.Data.Models
{
    public class PollVotedUsers
    {
        public string UserId { get; set; }

        public User User { get; set; }

        public string PollId { get; set; }

        public Poll Poll { get; set; }
    }
}