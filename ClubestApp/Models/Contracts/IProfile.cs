namespace ClubestApp.Models.Contracts
{
    public interface IProfile
    {
        string Username { get; set; }

        string FirstName { get; set; }

        string LastName { get; set; }

        string Email { get; set; }

        string PhoneNumber { get; set; }
    }
}