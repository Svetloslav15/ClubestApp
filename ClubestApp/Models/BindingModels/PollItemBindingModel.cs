namespace ClubestApp.Models.BindingModels
{
    using ClubestApp.Data.Models;
    using System;
    using System.Collections.Generic;

    public class PollItemBindingModel
    {
        public string Id { get; set; }

        public string Content { get; set; }

        public DateTime ExpiredDate { get; set; }

        public int VotesCount { get; set; }

        public List<Option> Options { get; set; }

        public bool IsMultichoice { get; set; }

        public List<PollVotedUsers> PollVotedUsers { get; set; }
    }
}