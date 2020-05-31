namespace ClubestApp.Models.InputModels
{
    using ClubestApp.Common;
    using Microsoft.AspNetCore.Http;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class AddClubInputModel
    {
        [Required(ErrorMessage = ErrorMessages.ClubNameRequired)]
        [StringLength(50, ErrorMessage = ErrorMessages.ClubNameRange, MinimumLength = 3)]
        public string Name { get; set; }

        [Required(ErrorMessage = ErrorMessages.ClubFeeRequired)]
        [Range(0, 1000, ErrorMessage = ErrorMessages.ClubFeeRange)]
        public decimal? Fee { get; set; }

        [Required(ErrorMessage = ErrorMessages.ClubIsPublicRequired)]
        public bool? IsPublic { get; set; }

        [Required(ErrorMessage = ErrorMessages.ClubPriceTypeRequired)]
        public string PriceType { get; set; }

        [Required(ErrorMessage = ErrorMessages.ClubDescriptionRequired)]
        [StringLength(300, ErrorMessage = ErrorMessages.ClubDescriptionRange, MinimumLength = 20)]
        public string Description { get; set; }

        [Required(ErrorMessage = ErrorMessages.ClubTownRequired)]
        public string Town { get; set; }

        public IFormFile ImageFile { get; set; }

        [Required(ErrorMessage = ErrorMessages.ClubInterestsRequired)]
        public List<string> Interests { get; set; } = new List<string>();

        public Dictionary<string, Dictionary<string, string>> InterestsToList { get; set; } = new Dictionary<string, Dictionary<string, string>>();
    }
}