namespace ClubestApp.Data
{
    using ClubestApp.Data.Models;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;

    public class ApplicationDbContext : IdentityDbContext<User, IdentityRole, string>
    {
        public DbSet<Club> Clubs { get; set; }

        public DbSet<ClubAdmin> ClubAdmins { get; set; }

        public DbSet<Comment> Comments { get; set; }

        public DbSet<Event> Events { get; set; }

        public DbSet<EventUser> EventUsers { get; set; }

        public DbSet<Notification> Notifications { get; set; }

        public DbSet<Option> Options { get; set; }

        public DbSet<Poll> Polls { get; set; }

        public DbSet<PollUser> PollUser { get; set; }

        public DbSet<PollVotedUsers> PollVotedUsers { get; set; }

        public DbSet<Post> Posts { get; set; }

        public DbSet<RequestNewClub> RequestNewClubs { get; set; }

        public DbSet<UserClub> UserClubs { get; set; }

        public DbSet<UserCommentLikes> UserCommentLikes { get; set; }

        public DbSet<UserCommentDislikes> UserCommentDislikes { get; set; }

        public DbSet<UserPostLikes> UserPostLikes { get; set; }

        public DbSet<UserPostDislikes> UserPostDislikes { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            //Define 1-M relations for User
            builder.Entity<User>()
                .HasMany(user => user.Posts)
                .WithOne(post => post.Author)
                .HasForeignKey(post => post.AuthorId);

            builder.Entity<User>()
                .HasMany(user => user.Notifications)
                .WithOne(notification => notification.User)
                .HasForeignKey(notification => notification.UserId);
            /**/

            //Define 1-M relations for Poll
            builder.Entity<Poll>()
                .HasMany(poll => poll.Options)
                .WithOne(option => option.Poll)
                .HasForeignKey(option => option.PollId);
            /**/

            //Define 1-M relations for Club
            builder.Entity<Club>()
                .HasMany(club => club.Events)
                .WithOne(e => e.Club)
                .HasForeignKey(e => e.ClubId);

            builder.Entity<Club>()
                .HasMany(club => club.Polls)
                .WithOne(poll => poll.Club)
                .HasForeignKey(poll => poll.ClubId);

            builder.Entity<Club>()
                .HasMany(club => club.Posts)
                .WithOne(post => post.Club)
                .HasForeignKey(post => post.ClubId);
            /**/

            //Define 1-M relations for Post
            builder.Entity<Post>()
                .HasMany(post => post.Comments)
                .WithOne(comment => comment.Post)
                .HasForeignKey(comment => comment.PostId);
            /**/

            //Define mapping table for Users and Clubs
            builder.Entity<UserClub>()
                .HasKey(uc => new { uc.UserId, uc.ClubId });

            builder.Entity<UserClub>()
                .HasOne(uc => uc.User)
                .WithMany(user => user.UserClubs)
                .HasForeignKey(user => user.UserId);

            builder.Entity<UserClub>()
                .HasOne(uc => uc.Club)
                .WithMany(user => user.ClubUsers)
                .HasForeignKey(user => user.ClubId);
            /**/

            //Define mapping table for Clubs and their's Admins
            builder.Entity<ClubAdmin>()
            .HasKey(uc => new { uc.UserId, uc.ClubId });

            builder.Entity<ClubAdmin>()
                .HasOne(uc => uc.User)
                .WithMany(user => user.AdminClubs)
                .HasForeignKey(user => user.UserId);

            builder.Entity<ClubAdmin>()
                .HasOne(ca => ca.Club)
                .WithMany(ca => ca.ClubAdmins)
                .HasForeignKey(ca => ca.ClubId);
            /**/

            //Define mapping table for User and Poll
            builder.Entity<PollUser>()
            .HasKey(uc => new { uc.UserId, uc.PollId });

            builder.Entity<PollUser>()
                .HasOne(pu => pu.User)
                .WithMany(pu => pu.UserPolls)
                .HasForeignKey(pu => pu.UserId);

            builder.Entity<PollUser>()
                .HasOne(pu => pu.Poll)
                .WithMany(pu => pu.PollUsers)
                .HasForeignKey(pu => pu.PollId);
            /**/

            //Define mapping table for Voted Users and Polls
            builder.Entity<PollVotedUsers>()
            .HasKey(uc => new { uc.UserId, uc.PollId });

            builder.Entity<PollVotedUsers>()
                .HasOne(pu => pu.User)
                .WithMany(pu => pu.UserVotedPolls)
                .HasForeignKey(pu => pu.UserId);

            builder.Entity<PollVotedUsers>()
                .HasOne(pu => pu.Poll)
                .WithMany(pu => pu.PollVotedUsers)
                .HasForeignKey(pu => pu.PollId);
            /**/

            //Define mapping table for User and Event
            builder.Entity<EventUser>()
            .HasKey(eu => new { eu.UserId, eu.EventId });

            builder.Entity<EventUser>()
                .HasOne(eu => eu.User)
                .WithMany(eu => eu.UserEvents)
                .HasForeignKey(eu => eu.UserId);

            builder.Entity<EventUser>()
                .HasOne(eu => eu.Event)
                .WithMany(eu => eu.EventUsers)
                .HasForeignKey(eu => eu.EventId);
            /**/

            //Define mapping table for User and Comment Likes
            builder.Entity<UserCommentLikes>()
            .HasKey(uc => new { uc.UserId, uc.CommentId });

            builder.Entity<UserCommentLikes>()
                .HasOne(uc => uc.User)
                .WithMany(uc => uc.UserCommentLikes)
                .HasForeignKey(uc => uc.UserId);

            builder.Entity<UserCommentLikes>()
                .HasOne(uc => uc.Comment)
                .WithMany(uc => uc.UserCommentLikes)
                .HasForeignKey(uc => uc.CommentId);
            /**/

            //Define mapping table for User and Comment Dislikes
            builder.Entity<UserCommentDislikes>()
            .HasKey(uc => new { uc.UserId, uc.CommentId });

            builder.Entity<UserCommentDislikes>()
                .HasOne(uc => uc.User)
                .WithMany(uc => uc.UserCommentDislikes)
                .HasForeignKey(uc => uc.UserId);

            builder.Entity<UserCommentDislikes>()
                .HasOne(uc => uc.Comment)
                .WithMany(uc => uc.UserCommentDislikes)
                .HasForeignKey(uc => uc.CommentId);
            /**/

            //Define mapping table for User and Post Likes
            builder.Entity<UserPostLikes>()
            .HasKey(up => new { up.UserId, up.PostId });

            builder.Entity<UserPostLikes>()
                .HasOne(up => up.User)
                .WithMany(up => up.UserPostLikes)
                .HasForeignKey(up => up.UserId);

            builder.Entity<UserPostLikes>()
                .HasOne(up => up.Post)
                .WithMany(up => up.UserPostLikes)
                .HasForeignKey(up => up.PostId);
            /**/

            //Define mapping table for User and Post Dislikes
            builder.Entity<UserPostDislikes>()
            .HasKey(up => new { up.UserId, up.PostId });

            builder.Entity<UserPostDislikes>()
                .HasOne(up => up.User)
                .WithMany(up => up.UserPostDislikes)
                .HasForeignKey(up => up.UserId);

            builder.Entity<UserPostDislikes>()
                .HasOne(up => up.Post)
                .WithMany(up => up.UserPostDislikes)
                .HasForeignKey(up => up.PostId);
            /**/
        }
    }
}