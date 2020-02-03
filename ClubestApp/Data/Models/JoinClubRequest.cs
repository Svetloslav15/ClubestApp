namespace ClubestApp.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class JoinClubRequest
    {
        public JoinClubRequest()
        {
            this.Id = Guid.NewGuid().ToString();
            this.IsApproved = false;
        }

        [Key]
        public string Id { get; set; }

        public string UserId { get; set; }

        public User User { get; set; }

        public string ClubId { get; set; }

        public Club Club { get; set; }

        public bool IsApproved { get; set; }
    }
}