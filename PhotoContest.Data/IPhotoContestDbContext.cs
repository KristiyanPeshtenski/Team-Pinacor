namespace PhotoContest.Data
{
    using System.Data.Entity;
    using Models;

    public interface IPhotoContestDbContext
    {
        IDbSet<User> Users { get; set; }
        
        IDbSet<Contest> Contests { get; set; }

        IDbSet<Photo> Photos { get; set; }

        IDbSet<Vote> Votes { get; set; }

        int SaveChanges();
    }
}