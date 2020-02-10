namespace ClubestApp.Data.Models
{
    using ClubestApp.Data.Models.Enums;
    using System;
    using System.ComponentModel.DataAnnotations;

    public class JoinClubRequest
    {
        public JoinClubRequest()
        {
            this.Id = Guid.NewGuid().ToString();
            this.RequestType = RequestType.Waiting;
        }

        [Key]
        public string Id { get; set; }

        public string UserId { get; set; }

        public User User { get; set; }

        public string ClubId { get; set; }

        public Club Club { get; set; }

        public RequestType RequestType { get; set; }
    }
}