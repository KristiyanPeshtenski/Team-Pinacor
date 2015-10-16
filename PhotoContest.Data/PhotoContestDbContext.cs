﻿namespace PhotoContest.Data
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

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Contest>()
                .HasMany(x => x.Participants)
                .WithMany(x => x.ContestsParticipateIn);

            modelBuilder.Entity<Contest>()
                .HasMany(x => x.Winners)
                .WithMany(x => x.WinContests);

            modelBuilder.Entity<Contest>()
                .HasRequired(x => x.Creator)
                .WithMany(x => x.OwnContests)
                .WillCascadeOnDelete(false);
                
            base.OnModelCreating(modelBuilder);
        }

        public static PhotoContestDbContext Create()
        {
            return new PhotoContestDbContext();
        }
    }
}
