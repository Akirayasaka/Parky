using System.Linq.Expressions;

namespace ParkyAPI.Repository.IRepository
{
    public interface IRepository<T> where T : class
    {
        /// <summary>
        /// 根據類別取資料
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        T Get(int id);

        /// <summary>
        /// 根據類別取得全部資料
        /// </summary>
        /// <returns></returns>
        ICollection<T> GetAll(Expression<Func<T, bool>>? filter,
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy);

        /// <summary>
        /// 根據條件篩選
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="includeProperties"></param>
        /// <returns></returns>
        T GetFirstOrDefault(Expression<Func<T, bool>>? filter, string? includeProperties);

        /// <summary>
        /// 新增資料
        /// </summary>
        /// <param name="entity"></param>
        void Add(T entity);

        /// <summary>
        /// 刪除資料
        /// </summary>
        /// <param name="id"></param>
        void Remove(int id);

        /// <summary>
        /// 刪除資料
        /// </summary>
        /// <param name="entity"></param>
        void Remove(T entity);
    }
}
