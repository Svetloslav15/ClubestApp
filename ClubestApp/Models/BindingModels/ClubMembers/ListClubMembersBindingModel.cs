namespace ClubestApp.Models.BindingModels
{
    using ClubestApp.Data.Models;
    using System.Collections.Generic;

    public class ListClubMembersBindingModel
    {
        public IList<Message> Messages { get; set; } = new List<Message>();

        public List<MemberItemBindingModel> Members { get; set; } = new List<MemberItemBindingModel>();

        public Club Club { get; set; }

        public string ClubPriceType { get; set; }

        public IList<ClubAdmin> ClubAdmins { get; set; } = new List<ClubAdmin>();
    }
}