namespace Clubest.Data.Models
{
    using Clubest.Data.Data.Models;

    public class UsersPolls
    {
        public string UserId { get; set; }

        public ApplicationUser User { get; set; }

        public string PollId { get; set; }

        public Poll Poll { get; set; }
    }
}