using Microsoft.EntityFrameworkCore;
using ParkyAPI.Repository.IRepository;
using System.Linq.Expressions;

namespace ParkyAPI.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly DbContext DbContext;
        internal DbSet<T> DbSet;

        public Repository(DbContext context)
        {
            DbContext = context;
            DbSet = context.Set<T>();
        }

        public void Add(T entity)
        {
            DbSet.Add(entity);
        }

        public T Get(int id)
        {
            return DbSet.Find(id);
        }

        public ICollection<T> GetAll(Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null)
        {
            IQueryable<T> query = DbSet;
            if (filter != null)
            {
                query = query.Where(filter);
            }
            return orderBy != null ? orderBy(query).ToList() : query.ToList();
        }

        public T GetFirstOrDefault(Expression<Func<T, bool>> filter = null, string includeProperties = null)
        {
            IQueryable<T> query = DbSet;
            if (filter != null)
            {
                query = query.Where(filter);
            }
            return query.FirstOrDefault();
        }

        public void Remove(int id)
        {
            T target = DbSet.Find(id);
            DbSet.Remove(target);
        }

        public void Remove(T entity)
        {
            DbSet.Remove(entity);
        }
    }
}
