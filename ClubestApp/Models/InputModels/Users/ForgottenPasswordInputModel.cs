namespace ClubestApp.Models.InputModels.Users
{
    using ClubestApp.Common;
    using System.ComponentModel.DataAnnotations;

    public class ForgottenPasswordInputModel
    {
        [Required(ErrorMessage = ErrorMessages.EmailRequired)]
        [EmailAddress]
        [Display(Name = UserFields.Email)]
        public string Email { get; set; }
    }
}