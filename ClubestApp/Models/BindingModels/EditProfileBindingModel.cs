using ClubestApp.Models.Contracts;

namespace ClubestApp.Models.BindingModels
{
    public class EditProfileBindingModel : IProfile
    {
        public string Username { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }
    }
}