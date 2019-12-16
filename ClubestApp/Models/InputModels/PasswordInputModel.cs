namespace ClubestApp.Models.InputModels
{
    using ClubestApp.Common;
    using System.ComponentModel.DataAnnotations;

    public class PasswordInputModel
    {
        public string Username { get; set; }

        [Required(ErrorMessage = ErrorMessages.PasswordRequired)]
        [StringLength(100, ErrorMessage = ErrorMessages.MinLength, MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string OldPassword { get; set; }

        [Required(ErrorMessage = ErrorMessages.PasswordRequired)]
        [StringLength(100, ErrorMessage = ErrorMessages.MinLength, MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare(UserFields.Password, ErrorMessage = ErrorMessages.PasswordsDontMatch)]
        public string ConfirmPassword { get; set; }
    }
}