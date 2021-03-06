﻿namespace ClubestApp.Data.Models
{
    using ClubestApp.Data.Models.Enums;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Club
    {
        public Club()
        {
            this.Id = Guid.NewGuid().ToString();
            this.ClubUsers = new HashSet<UserClub>();
            this.Events = new HashSet<Event>();
            this.ClubAdmins = new HashSet<ClubAdmin>();
            this.Polls = new HashSet<Poll>();
            this.Posts = new HashSet<Post>();
            this.JoinClubRequests = new HashSet<JoinClubRequest>();
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

        public ICollection<UserClub> ClubUsers { get; set; }

        public ICollection<Event> Events { get; set; }

        public ICollection<ClubAdmin> ClubAdmins { get; set; }

        public ICollection<Poll> Polls { get; set; }

        public ICollection<Post> Posts { get; set; }

        public PriceType PriceType { get; set; }
        
        public ICollection<JoinClubRequest> JoinClubRequests { get; set; }

        public ICollection<Message> Messages { get; set; }
    }
}