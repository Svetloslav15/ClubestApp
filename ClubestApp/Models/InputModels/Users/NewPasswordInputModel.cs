namespace ClubestApp.Models.InputModels.Users
{
    using ClubestApp.Common;
    using System.ComponentModel.DataAnnotations;

    public class NewPasswordInputModel
    {
        [Required(ErrorMessage = ErrorMessages.PasswordRequired)]
        [DataType(DataType.Password)]
        [Display(Name = UserFields.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = ErrorMessages.PasswordRequired)]
        [DataType(DataType.Password)]
        [Display(Name = UserFields.Password)]
        public string ConfirmPassword { get; set; }

        [Required]
        public string PasswordTokenId { get; set; }
    }
}