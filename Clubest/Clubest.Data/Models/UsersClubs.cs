namespace Clubest.Data.Models
{
    using Clubest.Data.Data.Models;

    public class UsersClubs
    {
        public string UserId { get; set; }

        public ApplicationUser User { get; set; }

        public string ClubId { get; set; }

        public Club Club { get; set; }
    }
}