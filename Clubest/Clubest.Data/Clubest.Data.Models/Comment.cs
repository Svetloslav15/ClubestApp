namespace Clubest.Data.Models
{
    using Clubest.Data.Data.Models;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    
    public class Comment
    {
        public Comment()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Likes = new List<ApplicationUser>();
            this.DisLikes = new List<ApplicationUser>();
        }

        [Key]
        public string Id { get; set; }

        public string Content { get; set; }

        public ICollection<ApplicationUser> Likes { get; set; }

        public ICollection<ApplicationUser> DisLikes { get; set; }

        public string AuthorId { get; set; }

        public ApplicationUser Author { get; set; }

        public string PostId { get; set; }

        public Post Post { get; set; }

        public DateTime Date { get; set; }
    }
}