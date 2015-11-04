using System.Data.Entity.ModelConfiguration.Conventions;

namespace PhotoContest.Data
{
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models;
    using Migrations;
    using System.Data.Entity;

    public class PhotoContestDbContext : IdentityDbContext<User>, IPhotoContestDbContext
    {
        public PhotoContestDbContext()
            : base("DefaultConnection", false)
        {
            Database.SetInitializer(
                new MigrateDatabaseToLatestVersion<PhotoContestDbContext, Configuration>());
        }

        public IDbSet<Contest> Contests { get; set; }

        public IDbSet<Photo> Photos { get; set; }

        public IDbSet<Vote> Votes { get; set; }

        public IDbSet<Notification> Notifications { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();

            modelBuilder.Entity<Contest>()
                .HasMany(x => x.Participants)
                .WithMany(x => x.ContestsParticipateIn)
                .Map(contest => contest.MapLeftKey("ContestId").MapRightKey("UserId")
                    .ToTable("ContestParticipants"));

            modelBuilder.Entity<Contest>()
                .HasMany(x => x.Winners)
                .WithMany(x => x.WinContests)
                .Map(contest => contest.MapLeftKey("ContestId").MapRightKey("UserId")
                    .ToTable("ContestWinners"));

            modelBuilder.Entity<Contest>()
                .HasRequired(x => x.Creator)
                .WithMany(x => x.OwnContests)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Notification>()
                .HasRequired(x => x.Sender)
                .WithMany(x => x.SentNotifications)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Notification>()
                .HasRequired(x => x.Receiver)
                .WithMany(x => x.ReceivedNotifications)
                .WillCascadeOnDelete(false);

            base.OnModelCreating(modelBuilder);
        }

        public static PhotoContestDbContext Create()
        {
            return new PhotoContestDbContext();
        }
    }
}

//modelBuilder.Entity<User>()
//                .HasMany(u => u.FavouriteTweens)
//                .WithMany(t => t.FavoritedBy)
//                .Map(u => u.MapLeftKey("UserId").MapRightKey("FavoriteTweenId")
//                .ToTable("UsersFavoriteTweens"));