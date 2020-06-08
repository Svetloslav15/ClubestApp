namespace ClubestApp.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class PasswordToken
    {
        public PasswordToken()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        [Key]
        public string Id { get; set; }
        
        public DateTime CreatedAt { get; set; }

        public string UserId { get; set; }

        public User User { get; set; }
    }
}