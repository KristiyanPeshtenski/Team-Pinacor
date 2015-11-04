namespace PhotoContest.Data.UnitOfWork
{
    using Repository;
    using Models;

    public interface IPhotoContestData
    {
        IGenericRepository<User> Users { get; } 

        IGenericRepository<Contest> Contests { get; }

        IGenericRepository<Vote> Votes { get; }

        IGenericRepository<Photo> Photos { get; }

        IGenericRepository<Notification> Notifications { get; }

        int SaveChanges();
    }
}