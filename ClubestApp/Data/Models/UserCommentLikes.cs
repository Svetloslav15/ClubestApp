namespace ClubestApp.Data.Models
{
    public class UserCommentLikes
    {
        public string UserId { get; set; }

        public User User { get; set; }

        public string CommentId { get; set; }

        public Comment Comment { get; set; }
    }
}