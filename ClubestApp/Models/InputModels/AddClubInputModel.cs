namespace ClubestApp.Models.InputModels
{
    using ClubestApp.Common;
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
    }
}