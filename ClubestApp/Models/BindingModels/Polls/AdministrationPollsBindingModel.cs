namespace ClubestApp.Models.BindingModels
{
    using ClubestApp.Data.Models;
    using System.Collections.Generic;

    public class AdministrationPollsBindingModel
    {
        public Club Club { get; set; }

        public string ClubPriceType { get; set; }

        public List<Poll> Polls { get; set; }

        public bool Expired { get; set; }

        public IList<Message> Messages { get; set; }
    }
}
