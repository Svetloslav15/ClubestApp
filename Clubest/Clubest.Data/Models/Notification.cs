namespace Clubest.Data.Models
{
    using Clubest.Data.Data.Models;
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Notification
    {
        public Notification()
        {
            this.Id = Guid.NewGuid().ToString();
            this.IsRead = false;
        }

        [Key]
        public string Id { get; set; }

        public string Content { get; set; }

        public string Link { get; set; }

        public string UserId { get; set; }
        
        public ApplicationUser User { get; set; }

        public bool IsRead { get; set; }
    }
}