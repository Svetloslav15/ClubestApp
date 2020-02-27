namespace ClubestApp.Data.Models
{
    using Microsoft.AspNetCore.Identity;
    using System.Collections.Generic;

    public class User : IdentityUser
    {
        public User()
        {
            this.Posts = new HashSet<Post>();
            this.UserClubs = new HashSet<UserClub>();
            this.AdminClubs = new HashSet<ClubAdmin>();
            this.Notifications = new HashSet<Notification>();
            this.UserEvents = new HashSet<EventUser>();
            this.RequestNewClubs = new HashSet<RequestNewClub>();
            this.UserCommentLikes = new HashSet<UserCommentLikes>();
            this.UserCommentDislikes = new HashSet<UserCommentDislikes>();
            this.UserPolls = new HashSet<PollUser>();
            this.UserVotedPolls = new HashSet<PollVotedUsers>();
            this.UserPostLikes = new HashSet<UserPostLikes>();
            this.UserPostDislikes = new HashSet<UserPostDislikes>();
            this.JoinClubRequests = new HashSet<JoinClubRequest>();
            this.EventRoles = new HashSet<EventRole>();
        }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Town { get; set; }

        public string Interests { get; set; }

        public string PictureUrl { get; set; }

        public ICollection<Post> Posts { get; set; }

        public ICollection<UserClub> UserClubs { get; set; }

        public ICollection<ClubAdmin> AdminClubs { get; set; }

        public ICollection<Notification> Notifications { get; set; }

        public ICollection<PollUser> UserPolls { get; set; }

        public ICollection<PollVotedUsers> UserVotedPolls { get; set; }

        public ICollection<EventUser> UserEvents { get; set; }

        public ICollection<RequestNewClub> RequestNewClubs { get; set; }

        public ICollection<UserCommentLikes> UserCommentLikes { get; set; }

        public ICollection<UserCommentDislikes> UserCommentDislikes { get; set; }

        public ICollection<UserPostLikes> UserPostLikes { get; set; }

        public ICollection<UserPostDislikes> UserPostDislikes { get; set; }

        public ICollection<JoinClubRequest> JoinClubRequests { get; set; }

        public ICollection<EventRole> EventRoles { get; set; }
    }
}