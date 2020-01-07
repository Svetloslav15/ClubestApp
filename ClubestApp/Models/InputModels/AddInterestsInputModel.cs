namespace ClubestApp.Models.InputModels
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class AddInterestsInputModel
    {
        [Required]
        public List<string> Interests { get; set; }
    }
}