namespace ClubestApp.Data.Models
{
    public class PollUser
    {
        public string UserId { get; set; }

        public User User { get; set; }

        public string PollId { get; set; }

        public Poll Poll { get; set; }
    }
}