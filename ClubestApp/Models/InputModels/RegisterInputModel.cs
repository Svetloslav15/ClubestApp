namespace ClubestApp.Models.InputModels
{
    using ClubestApp.Common;
    using System.ComponentModel.DataAnnotations;

    public class RegisterInputModel
    {
        [Required(ErrorMessage = ErrorMessages.FirstNameRequired)]
        [Display(Name = UserFields.FirstName)]
        public string FirstName { get; set; }
        
        [Required(ErrorMessage = ErrorMessages.LastNameRequired)]
        [Display(Name = UserFields.LastName)]
        public string LastName { get; set; }

        [Required(ErrorMessage = ErrorMessages.EmailRequired)]
        [EmailAddress]
        [Display(Name = UserFields.Email)]
        public string Email { get; set; }

        [Required(ErrorMessage = ErrorMessages.PasswordRequired)]
        [StringLength(100, ErrorMessage = ErrorMessages.MinLength, MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = UserFields.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = UserFields.ConfirmPassword)]
        [Compare(UserFields.Password, ErrorMessage = ErrorMessages.PasswordsDontMatch)]
        public string ConfirmPassword { get; set; }
    }
}