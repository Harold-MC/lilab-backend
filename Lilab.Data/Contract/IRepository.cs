using System.Linq.Expressions;
using Lilab.Service.Contract;

namespace Lilab.Data.Contract
{
    public interface IRepository {}
    
    public interface IRepository<T> : IRepository
        where T : class, new()
    {
        T Add(T entity);

        Task<T> AddAsync(T entity);

        IEnumerable<T> AddRange(IEnumerable<T> entityList);

        Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entityList);

        void RemoveAll(IEnumerable<T> entities);

        Task RemoveAllAsync(IEnumerable<T> entities);

        void Remove(T entity);

        Task RemoveAsync(T entity);

        T Update(T entity);

        Task<T> UpdateAsync(T entity);

        IQueryable<T> GetAll(string sortExpression = null);

        Task<IEnumerable<T>> GetAllAsync(string sortExpression = null);

        IQueryable<T> GetAll(Expression<Func<T, bool>> filter, string sortExpression = null);

        Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> filter, string sortExpression = null);

        IQueryable<T> GetAll(Func<IQueryable<T>, IQueryable<T>> transform, Expression<Func<T, bool>> filter = null, string sortExpression = null);

        Task<IEnumerable<T>> GetAllAsync(Func<IQueryable<T>, IQueryable<T>> transform, Expression<Func<T, bool>> filter = null, string sortExpression = null);

        IQueryable<TResult> GetAll<TResult>(Func<IQueryable<T>, IQueryable<TResult>> transform, Expression<Func<T, bool>> filter = null, string sortExpression = null);

        Task<IEnumerable<TResult>> GetAllAsync<TResult>(Func<IQueryable<T>, IQueryable<TResult>> transform, Expression<Func<T, bool>> filter = null, string sortExpression = null);

        IPagedList<T> GetPaged(int pageIndex, int pageSize, string sortExpression = null);

        Task<IPagedList<T>> GetPagedAsync(int pageIndex, int pageSize, string sortExpression = null);

        IPagedList<T> GetPaged(Func<IQueryable<T>, IQueryable<T>> transform, Expression<Func<T, bool>> filter = null, int pageIndex = -1, int pageSize = -1, string sortExpression = null);

        Task<IPagedList<T>> GetPagedAsync(Func<IQueryable<T>, IQueryable<T>> transform, Expression<Func<T, bool>> filter = null, int pageIndex = -1, int pageSize = -1, string sortExpression = null);

        IPagedList<TResult> GetPaged<TResult>(Func<IQueryable<T>, IQueryable<TResult>> transform, Expression<Func<T, bool>> filter = null, int pageIndex = -1, int pageSize = -1, string sortExpression = null);

        Task<IPagedList<TResult>> GetPagedAsync<TResult>(Func<IQueryable<T>, IQueryable<TResult>> transform, Expression<Func<T, bool>> filter = null, int pageIndex = -1, int pageSize = -1, string sortExpression = null);

        int GetCount<TResult>(Func<IQueryable<T>, IQueryable<TResult>> transform, Expression<Func<T, bool>> filter = null);

        Task<int> GetCountAsync<TResult>(Func<IQueryable<T>, IQueryable<TResult>> transform, Expression<Func<T, bool>> filter = null);

        TResult Get<TResult>(Func<IQueryable<T>, IQueryable<TResult>> transform, Expression<Func<T, bool>> filter = null, string sortExpression = null);

        Task<TResult> GetAsync<TResult>(Func<IQueryable<T>, IQueryable<TResult>> transform, Expression<Func<T, bool>> filter = null, string sortExpression = null);

        bool Exists<TResult>(Func<IQueryable<T>, IQueryable<TResult>> transform, Expression<Func<T, bool>> filter = null);

        Task<bool> ExistsAsync<TResult>(Func<IQueryable<T>, IQueryable<TResult>> transform, Expression<Func<T, bool>> filter = null);

        bool Exists(Func<IQueryable<T>, IQueryable<T>> transform, Expression<Func<T, bool>> filter = null);

        Task<bool> ExistsAsync(Func<IQueryable<T>, IQueryable<T>> transform, Expression<Func<T, bool>> filter = null);
    }

}