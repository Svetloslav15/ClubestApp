namespace ClubestApp.Models.BindingModels.Home
{
    using ClubestApp.Data.Models;
    using System.Collections.Generic;

    public class IndexPageBindingModel
    {
        public IList<Post> Posts { get; set; } = new List<Post>();
    }
}