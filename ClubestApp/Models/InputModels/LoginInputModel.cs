namespace ClubestApp.Models.InputModels
{
    using ClubestApp.Common;
    using System.ComponentModel.DataAnnotations;

    public class LoginInputModel
    {
        [Required(ErrorMessage = ErrorMessages.EmailRequired)]
        [EmailAddress]
        [Display(Name = UserFields.Email)]
        public string Email { get; set; }

        [Required(ErrorMessage = ErrorMessages.PasswordRequired)]
        [DataType(DataType.Password)]
        [Display(Name = UserFields.Password)]
        public string Password { get; set; }
    }
}