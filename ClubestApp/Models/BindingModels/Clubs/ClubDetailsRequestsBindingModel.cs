﻿namespace ClubestApp.Models.BindingModels
{
    using ClubestApp.Data.Models;
    using System.Collections.Generic;

    public class ClubDetailsRequestsBindingModel
    {
        public Club Club { get; set; }

        public string ClubPriceType { get; set; }

        public RequestApproveBindingModel RequestApproveBindingModel { get; set; }

        public List<JoinClubRequest> JoinClubRequests { get; set; }

        public IList<Message> Messages { get; set; }
    }
}