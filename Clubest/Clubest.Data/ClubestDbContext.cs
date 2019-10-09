namespace Clubest.Data
{
    using Clubest.Data.Data.Models;
    using Clubest.Data.Models;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;

    public class ClubestDbContext : IdentityDbContext<ApplicationUser, IdentityRole, string>
    {
        public DbSet<Club> Clubs { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<Option> Options { get; set; }
        public DbSet<Poll> Polls { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<RequestNewClub> RequestNewClubs { get; set; }
        public DbSet<UsersClubs> UsersClubs { get; set; }
        public DbSet<UsersEvents> UsersEvents { get; set; }
        public DbSet<UsersPolls> UsersPolls { get; set; }

        public ClubestDbContext(DbContextOptions options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<ApplicationUser>()
                .HasMany(user => user.Posts)
                .WithOne(post => post.Author)
                .HasForeignKey(post => post.AuthorId);

            builder.Entity<ApplicationUser>()
               .HasMany(user => user.Notifications)
               .WithOne(notification => notification.User)
               .HasForeignKey(notification => notification.UserId);

            builder.Entity<ApplicationUser>()
              .HasMany(user => user.Comments)
              .WithOne(comment => comment.Author)
              .HasForeignKey(comment => comment.AuthorId);

            builder.Entity<ApplicationUser>()
              .HasMany(user => user.RequestNewClubs)
              .WithOne(request => request.Author)
              .HasForeignKey(request => request.AuthorId);

            builder.Entity<Club>()
                .HasMany(club => club.Posts)
                .WithOne(post => post.Club)
                .HasForeignKey(post => post.ClubId);

            builder.Entity<Club>()
                .HasMany(club => club.Events)
                .WithOne(ev => ev.Club)
                .HasForeignKey(ev => ev.ClubId);

            builder.Entity<Club>()
               .HasMany(club => club.Polls)
               .WithOne(poll => poll.Club)
               .HasForeignKey(poll => poll.ClubId);

            builder.Entity<Post>()
               .HasMany(post => post.Comments)
               .WithOne(comment => comment.Post)
               .HasForeignKey(comment => comment.PostId);

            builder.Entity<Poll>()
              .HasMany(poll => poll.Options)
              .WithOne(option => option.Poll)
              .HasForeignKey(option => option.PollId);

            builder.Entity<UsersClubs>()
                .HasKey(uc => new
                {
                    uc.ClubId,
                    uc.UserId
                });

            builder.Entity<UsersClubs>()
                .HasOne(uc => uc.User)
                .WithMany(u => u.UsersClubs)
                .HasForeignKey(u => u.UserId);

            builder.Entity<UsersClubs>()
                .HasOne(uc => uc.Club)
                .WithMany(c => c.UsersClubs)
                .HasForeignKey(c => c.ClubId);

            builder.Entity<UsersPolls>()
               .HasKey(uc => new
               {
                   uc.UserId,
                   uc.PollId
               });

            builder.Entity<UsersPolls>()
               .HasOne(up => up.User)
               .WithMany(u => u.UsersPolls)
               .HasForeignKey(u => u.UserId);

            builder.Entity<UsersPolls>()
                .HasOne(up => up.Poll)
                .WithMany(c => c.UsersPolls)
                .HasForeignKey(c => c.PollId);

            builder.Entity<UsersEvents>()
               .HasKey(ue => new
               {
                   ue.UserId,
                   ue.EventId
               });

            builder.Entity<UsersEvents>()
               .HasOne(up => up.User)
               .WithMany(u => u.UsersEvents)
               .HasForeignKey(u => u.UserId);

            builder.Entity<UsersEvents>()
                .HasOne(ue => ue.Event)
                .WithMany(e => e.UsersEvents)
                .HasForeignKey(e => e.EventId);
        }
    }
}