namespace ClubestApp.Models.BindingModels
{
    using ClubestApp.Data.Models;
    using System.Collections.Generic;

    public class ClubDetailsBindingModel
    {
        public Club Club { get; set; }

        public string ClubPriceType { get; set; }

        public IList<Post> Posts { get; set; } = new List<Post>();
    }
}