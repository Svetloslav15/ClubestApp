namespace ClubestApp.Data.Models
{
    using ClubestApp.Data.Models.Enums;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Post
    {
        public Post()
        {
            this.Id = Guid.NewGuid().ToString();
            this.UserPostLikes = new HashSet<UserPostLikes>();
            this.UserPostDislikes = new HashSet<UserPostDislikes>();
            this.Comments = new HashSet<Comment>();
        }

        [Key]
        public string Id { get; set; }

        public string Content { get; set; }

        public string FileUrlOrLink { get; set; }

        public PostType PostType { get; set; }

        public ICollection<UserPostLikes> UserPostLikes { get; set; }

        public ICollection<UserPostDislikes> UserPostDislikes { get; set; }

        public ICollection<Comment> Comments { get; set; }

        public string AuthorId { get; set; }

        public User Author { get; set; }

        public string ClubId { get; set; }

        public Club Club { get; set; }

        public DateTime DateTime { get; set; }
    }
}