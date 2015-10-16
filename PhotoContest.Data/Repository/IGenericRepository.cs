namespace PhotoContest.Data.Repository
{
    using System.Linq;

    public interface IGenericRepository<T> where T : class
    {
        IQueryable<T> All();

        T GetById(object id);

        void Add(T entity);

        void Update(T entity);

        void Delete(T entity);

        void DeleteById(object id);

        void Detach(T entity);
    }
}