using System.Collections.Generic;

namespace ClubestApp.Models.BindingModels
{
    public class EditInterestsBindingModel
    {
        public Dictionary<string, Dictionary<string, string>> AllInterests { get; set; }

        public List<string> UserInterests { get; set; }
    }
}
