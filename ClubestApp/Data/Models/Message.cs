using System;

namespace ClubestApp.Data.Models
{
    public class Message
    {
        public string Id { get; set; }

        public string Content { get; set; }

        public DateTime Date { get; set; }

        public string ClubId { get; set; }
        public Club Club { get; set; }

        public string SenderId { get; set; }
        public User Sender { get; set; }
    }
}
