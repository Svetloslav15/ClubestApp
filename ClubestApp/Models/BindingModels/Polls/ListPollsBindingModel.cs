namespace ClubestApp.Models.BindingModels
{
    using ClubestApp.Data.Models;
    using System;
    using System.Collections.Generic;

    public class ListPollsBindingModel
    {
        public List<PollItemBindingModel> Polls { get; set; }

        public Club Club { get; set; }

        public string ClubPriceType { get; set; }

        public string IsMultichoice { get; set; }

        public string Content { get; set; }

        public DateTime ExpiredDate { get; set; }

        public string Options { get; set; }

        public string ClubId { get; set; }

        public List<string> Votes { get; set; }

        public string PollId { get; set; }

        public string UserId { get; set; }

        public IList<Message> Messages { get; set; }
    }
}
