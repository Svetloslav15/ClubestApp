namespace ClubestApp.Models.BindingModels
{
    using Microsoft.AspNetCore.Http;
    using System.Collections.Generic;

    public class EditClubBindingModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public decimal? Fee { get; set; }

        public bool? IsPublic { get; set; }

        public string PriceType { get; set; }

        public string Description { get; set; }

        public string Town { get; set; }

        public string PictureUrl { get; set; }

        public IFormFile ImageFile { get; set; }

        public List<string> Interests { get; set; } = new List<string>();

        public Dictionary<string, Dictionary<string, string>> InterestsToList { get; set; } = new Dictionary<string, Dictionary<string, string>>();
    }
}
