namespace ClubestApp.Models.BindingModels.RequestNewClub
{
    using ClubestApp.Data.Models;
    using System.Collections.Generic;

    public class ListAllRequestsNewClubBindingModel
    {
        public IList<RequestNewClub> RequestsNewClub { get; set; } = new List<RequestNewClub>();
    }
}