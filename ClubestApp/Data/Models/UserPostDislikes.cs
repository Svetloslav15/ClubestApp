namespace ClubestApp.Data.Models
{
    public class UserPostDislikes
    {
        public string UserId { get; set; }

        public User User { get; set; }

        public string PostId { get; set; }

        public Post Post { get; set; }
    }
}