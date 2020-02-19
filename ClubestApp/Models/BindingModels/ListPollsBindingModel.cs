namespace ClubestApp.Models.BindingModels
{
    using ClubestApp.Data.Models;
    using System.Collections.Generic;

    public class ListPollsBindingModel
    {
        public List<PollItemBindingModel> Polls { get; set; }

        public Club Club { get; set; }

        public string ClubPriceType { get; set; }
    }
}
