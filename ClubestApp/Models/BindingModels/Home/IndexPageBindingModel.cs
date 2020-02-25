namespace ClubestApp.Models.BindingModels.Home
{
    using ClubestApp.Data.Models;
    using System.Collections.Generic;

    public class IndexPageBindingModel
    {
        public List<Post> Posts { get; set; } = new List<Post>();
    }
}