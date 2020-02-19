namespace ClubestApp.Models.BindingModels
{
    using System;

    public class PollItemBindingModel
    {
        public string Id { get; set; }

        public string Content { get; set; }

        public DateTime ExpiredDate { get; set; }

        public int VotesCount { get; set; }
    }
}