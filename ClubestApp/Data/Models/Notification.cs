namespace ClubestApp.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Notification
    {
        public Notification()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        [Key]
        public string Id { get; set; }

        public string Content { get; set; }

        public string Link { get; set; }

        public string UserId { get; set; }

        public User User { get; set; }

        public bool IsRead { get; set; }
    }
}