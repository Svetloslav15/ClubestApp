namespace ClubestApp.Models.InputModels
{
    using ClubestApp.Models.Contracts;
    using System.ComponentModel.DataAnnotations;

    public class EditProfileInputModel : IProfile
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        public string PhoneNumber { get; set; }
    }
}