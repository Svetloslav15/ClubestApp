namespace ClubestApp.Models.BindingModels
{
    using ClubestApp.Data.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class ListClubMembersBindingModel
    {
        public List<MemberItemBindingModel> Members { get; set; } = new List<MemberItemBindingModel>();

        public Club Club { get; set; }

        public string ClubPriceType { get; set; }

        public IList<ClubAdmin> ClubAdmins { get; set; } = new List<ClubAdmin>();
    }
}