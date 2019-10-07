namespace Clubest.Data.Models
{
    using Clubest.Data.Data.Models;
    using System;
    using System.ComponentModel.DataAnnotations;

    public class RequestNewClub
    {
        public RequestNewClub()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        [Key]
        public string Id { get; set; }

        public string Content { get; set; }

        public string Category { get; set; }

        public string AuthorId { get; set; }

        public ApplicationUser Author { get; set; }
    }
}