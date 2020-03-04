namespace ClubestApp.Models.BindingModels.User
{
    using System.Collections.Generic;

    public class MyClubsViewModel
    {
        public IEnumerable<GetClubsBindingModel> Clubs { get; set; }
    }
}
