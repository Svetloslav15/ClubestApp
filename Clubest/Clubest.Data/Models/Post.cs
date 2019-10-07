namespace Clubest.Data.Models
{
    using Clubest.Data.Data.Models;
    using Clubest.Data.Models.Enums;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Post
    {
        public Post()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Likes = new List<ApplicationUser>();
            this.DisLikes = new List<ApplicationUser>();
            this.Comments = new List<Comment>();
        }
        [Key]
        public string Id { get; set; }

        public string Title { get; set; }

        public FileType FileType { get; set; }

        public string Content { get; set; }

        public ICollection<ApplicationUser> Likes { get; set; }

        public ICollection<ApplicationUser> DisLikes { get; set; }

        public ICollection<Comment> Comments { get; set; }

        public string AuthorId { get; set; }
        
        public ApplicationUser Author { get; set; }

        public string ClubId { get; set; }
        
        public Club Club { get; set; }

        public DateTime Date { get; set; }
    }
}