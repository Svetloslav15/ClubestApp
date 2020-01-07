namespace ClubestApp.Models.InputModels
{
    using System.ComponentModel.DataAnnotations;

    public class AddClubInputModel
    {
        [Required]
        [MinLength(3), MaxLength(100)]
        public string Name { get; set; }

        [Required]
        [Range(0, 1000)]
        public decimal Fee { get; set; }

        [Required]
        public bool IsPublic { get; set; }

        [Required]
        public string PriceType { get; set; }
    }
}