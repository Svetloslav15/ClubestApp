namespace ClubestApp.Data.Models
{
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

        public string Name { get; set; }

        public decimal Fee { get; set; }

        public bool IsPublic { get; set; }

        public string Description { get; set; }

        public string Town { get; set; }

        public string Interests { get; set; }

        public string PictureUrl { get; set; }

        public string AuthorId { get; set; }

        public User Author { get; set; }
    }
}