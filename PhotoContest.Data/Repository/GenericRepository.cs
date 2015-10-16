namespace PhotoContest.Data.Repository
{
    using System.Data.Entity;
    using System.Linq;

    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private DbContext context;
        private IDbSet<T> set;

        public GenericRepository(DbContext db)
        {
            this.context = db;
            this.set = this.context.Set<T>();
        }

        public void Add(T entity)
        {
            this.ChangeState(entity, EntityState.Added);
        }

        public IQueryable<T> All()
        {
            return this.set;
        }

        public void Delete(T entity)
        {
            this.ChangeState(entity, EntityState.Deleted);
        }

        public void DeleteById(object id)
        {
            var entity = this.set.Find(id);
            this.ChangeState(entity, EntityState.Deleted);
        }

        public void Detach(T entity)
        {
            this.ChangeState(entity, EntityState.Detached);
        }

        public T GetById(object id)
        {
            return this.set.Find(id);
        }

        public void Update(T entity)
        {
            this.ChangeState(entity, EntityState.Modified);
        }

        private void ChangeState(T entity, EntityState state)
        {
            var entry = this.context.Entry(entity);
            if (EntityState.Detached == state)
            {
                this.set.Attach(entity);
            }

            entry.State = state;
        }
    }
}