namespace PhotoContest.Data.UnitOfWork
{
    using System;
    using System.Collections.Generic;
    using Repository;
    using Models;

    public class PhotoContestData : IPhotoContestData
    {
        private IPhotoContestDbContext db;
        private IDictionary<Type, object> repositories;

        public PhotoContestData(IPhotoContestDbContext ctx)
        {
            this.db = ctx;
            this.repositories = new Dictionary<Type, object>();
        }

        public IGenericRepository<User> Users
        {
            get { return this.GetRepository<User>(); }
        }

        public IGenericRepository<Contest> Contests
        {
            get { return this.GetRepository<Contest>(); }

        }

        public IGenericRepository<Vote> Votes
        {
            get { return this.GetRepository<Vote>(); }

        }

        public IGenericRepository<Photo> Photos
        {
            get { return this.GetRepository<Photo>(); }
        }

        public IGenericRepository<Notification> Notifications
        {
            get { return this.GetRepository<Notification>(); }
        }


        public int SaveChanges()
        {
            return this.db.SaveChanges();
        }

        private IGenericRepository<T> GetRepository<T>() where T : class
        {
            var modelType = typeof(T);
            if (!this.repositories.ContainsKey(modelType))
            {
                var repositoryType = typeof(GenericRepository<T>);
                this.repositories.Add(modelType, Activator.CreateInstance(repositoryType, this.db));
            }

            return (IGenericRepository<T>)this.repositories[modelType];
        }
    }
}