using System.Collections.Generic;

namespace ClubestApp.Models.BindingModels
{
    public class EditInterestsBindingModel
    {
        public Dictionary<string, Dictionary<string, string>> AllInterests { get; set; } = new Dictionary<string, Dictionary<string, string>>();

        public List<string> UserInterests { get; set; } = new List<string>();
    }
}
