namespace ClubestApp.Models.BindingModels
{
    using ClubestApp.Data.Models;
    using System.Collections.Generic;

    public class AdministrationPollsBindingModel
    {
        public Club Club { get; set; }

        public string ClubPriceType { get; set; }

        public List<Poll> ActivePolls { get; set; }

        public List<Poll> NonActivePolls { get; set; }
    }
}
