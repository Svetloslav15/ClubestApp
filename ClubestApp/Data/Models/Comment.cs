namespace ClubestApp.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Comment
    {
        public Comment()
        {
            this.Id = Guid.NewGuid().ToString();
            this.UserCommentLikes = new HashSet<UserCommentLikes>();
            this.UserCommentDislikes = new HashSet<UserCommentDislikes>();
        }

        [Key]
        public string Id { get; set; }

        public string Content { get; set; }

        public ICollection<UserCommentLikes> UserCommentLikes { get; set; }

        public ICollection<UserCommentDislikes> UserCommentDislikes { get; set; }

        public string AuthorId { get; set; }

        public User Author { get; set; }

        public string PostId { get; set; }

        public Post Post { get; set; }

        public DateTime Date { get; set; }
    }
}