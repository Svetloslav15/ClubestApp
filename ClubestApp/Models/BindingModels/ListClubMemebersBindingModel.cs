using ClubestApp.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClubestApp.Models.BindingModels
{
    public class ListClubMemebersBindingModel
    {
        public List<MemberItemBindingModel> Members { get; set; }

        public Club Club { get; set; }

        public string ClubPriceType { get; set; }
    }
}
