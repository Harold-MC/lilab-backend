using System.Linq.Expressions;
using Lilab.Data.Contract;
using Lilab.Service.Contract;
using Microsoft.EntityFrameworkCore;

namespace Lilab.Data.Repository
{
    public abstract class BaseRepository<TEntity, U> : IRepository<TEntity>
        where TEntity : class, new()
        where U : DbContext
    {
        protected readonly U _Context;
        private readonly DbSet<TEntity> _DbSet;

        protected BaseRepository(U context)
        {
            _Context = context;
            _DbSet = _Context.Set<TEntity>();
        }
        
        public virtual void MarkAsModified(TEntity entity)
        {
            _DbSet.Attach(entity);
            _Context.Entry(entity).State = EntityState.Modified;

            _Context.SaveChanges();
        }
        
        public virtual void Attach(TEntity entity)
        {
            _DbSet.Attach(entity);
        }

        public virtual TEntity Add(TEntity entity)
        {
            _Context.Set<TEntity>().Add(entity);

            _Context.SaveChanges();

            return entity;
        }

        public virtual async Task<TEntity> AddAsync(TEntity entity)
        {
            await _Context.Set<TEntity>().AddAsync(entity);

            await _Context.SaveChangesAsync();

            return entity;
        }

        public virtual IEnumerable<TEntity> AddRange(IEnumerable<TEntity> entityList)
        {
            _Context.Set<TEntity>().AddRange(entityList);

            _Context.SaveChanges();

            return entityList;
        }

        public virtual async Task<IEnumerable<TEntity>> AddRangeAsync(IEnumerable<TEntity> entityList)
        {
            await _Context.Set<TEntity>().AddRangeAsync(entityList);

            await _Context.SaveChangesAsync();

            return entityList;
        }

        public virtual void Remove(TEntity entity)
        {
            _DbSet.Attach(entity);
            _Context.Entry<TEntity>(entity).State = EntityState.Deleted;

            _Context.SaveChanges();
        }

        public virtual async Task RemoveAsync(TEntity entity)
        {
            _DbSet.Attach(entity);

            _Context.Entry<TEntity>(entity).State = EntityState.Deleted;

            await _Context.SaveChangesAsync();
        }

        public void RemoveAll(IEnumerable<TEntity> entities)
        {
            foreach(TEntity current in entities)
            {
                _DbSet.Attach(current);
                _Context.Entry<TEntity>(current).State = EntityState.Deleted;

            }

            _Context.SaveChanges();
        }

        public async Task RemoveAllAsync(IEnumerable<TEntity> entities)
        {
            foreach(TEntity current in entities)
            {
                _DbSet.Attach(current);
                _Context.Entry<TEntity>(current).State = EntityState.Deleted;

            }

            await _Context.SaveChangesAsync();
        }

        public virtual TEntity Update(TEntity entity)
        {
            _DbSet.Attach(entity);
            _Context.Entry<TEntity>(entity).State = EntityState.Modified;

            _Context.SaveChanges();

            return entity;
        }

        public virtual async Task<TEntity> UpdateAsync(TEntity entity)
        {
            _DbSet.Attach(entity);
            _Context.Entry<TEntity>(entity).State = EntityState.Modified;

            await _Context.SaveChangesAsync();

            return entity;
        }

        public IQueryable<TEntity> GetAll(string sortExpression = null)
        {
            var query = _DbSet;

            //Breaking Changes in EF Core 3: The Query Translator for EF Core 3 was changed and the query building
            //and evaluation is different. Now, when you try to order by a property name string then it crashes.
            //If an update fixes this or a workaround is found, then implement it into the OrderBy Extension
            //and uncomment the line below.

            return query;
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync(string sortExpression = null)
        {
            var query = _DbSet;

            //Breaking Changes in EF Core 3: The Query Translator for EF Core 3 was changed and the query building
            //and evaluation is different. Now, when you try to order by a property name string then it crashes.
            //If an update fixes this or a workaround is found, then implement it into the OrderBy Extension
            //and uncomment the line below.

            return await query.ToListAsync();
        }

        public IQueryable<TEntity> GetAll(Expression<Func<TEntity, bool>> filter, string sortExpression = null)
        {
            var query = _DbSet.AsNoTracking().Where(filter);

            //Breaking Changes in EF Core 3: The Query Translator for EF Core 3 was changed and the query building
            //and evaluation is different. Now, when you try to order by a property name string then it crashes.
            //If an update fixes this or a workaround is found, then implement it into the OrderBy Extension
            //and uncomment the line below.

            return query;
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> filter, string sortExpression = null)
        {
            var query = _DbSet.AsNoTracking().Where(filter);

            //Breaking Changes in EF Core 3: The Query Translator for EF Core 3 was changed and the query building
            //and evaluation is different. Now, when you try to order by a property name string then it crashes.
            //If an update fixes this or a workaround is found, then implement it into the OrderBy Extension
            //and uncomment the line below.

            return await query.ToListAsync();
        }

        public IQueryable<TEntity> GetAll(Func<IQueryable<TEntity>, IQueryable<TEntity>> transform, Expression<Func<TEntity, bool>> filter = null, string sortExpression = null)
        {
            var query = filter == null ? _DbSet.AsNoTracking() : _DbSet.AsNoTracking().Where(filter);

            var notSortedResults = transform(query);

            //Breaking Changes in EF Core 3: The Query Translator for EF Core 3 was changed and the query building
            //and evaluation is different. Now, when you try to order by a property name string then it crashes.
            //If an update fixes this or a workaround is found, then implement it into the OrderBy Extension
            //and uncomment the line below.

            return notSortedResults;
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync(Func<IQueryable<TEntity>, IQueryable<TEntity>> transform, Expression<Func<TEntity, bool>> filter = null, string sortExpression = null)
        {
            var query = filter == null ? _DbSet.AsNoTracking() : _DbSet.AsNoTracking().Where(filter);

            var notSortedResults = transform(query);

            //Breaking Changes in EF Core 3: The Query Translator for EF Core 3 was changed and the query building
            //and evaluation is different. Now, when you try to order by a property name string then it crashes.
            //If an update fixes this or a workaround is found, then implement it into the OrderBy Extension
            //and uncomment the line below.

            return await notSortedResults.ToListAsync();
        }

        public IQueryable<TResult> GetAll<TResult>(Func<IQueryable<TEntity>, IQueryable<TResult>> transform, Expression<Func<TEntity, bool>> filter = null, string sortExpression = null)
        {
            var query = filter == null ? _DbSet.AsNoTracking() : _DbSet.AsNoTracking().Where(filter);

            var notSortedResults = transform(query);

            //Breaking Changes in EF Core 3: The Query Translator for EF Core 3 was changed and the query building
            //and evaluation is different. Now, when you try to order by a property name string then it crashes.
            //If an update fixes this or a workaround is found, then implement it into the OrderBy Extension
            //and uncomment the line below.

            return notSortedResults;
        }

        public async Task<IEnumerable<TResult>> GetAllAsync<TResult>(Func<IQueryable<TEntity>, IQueryable<TResult>> transform, Expression<Func<TEntity, bool>> filter = null, string sortExpression = null)
        {
            var query = filter == null ? _DbSet.AsNoTracking() : _DbSet.AsNoTracking().Where(filter);

            var notSortedResults = transform(query);

            //Breaking Changes in EF Core 3: The Query Translator for EF Core 3 was changed and the query building
            //and evaluation is different. Now, when you try to order by a property name string then it crashes.
            //If an update fixes this or a workaround is found, then implement it into the OrderBy Extension
            //and uncomment the line below.

            return await notSortedResults.ToListAsync();
        }

        public IPagedList<TEntity> GetPaged(int pageIndex, int pageSize, string sortExpression = null)
        {
            var query = _DbSet.AsNoTracking();

            //Breaking Changes in EF Core 3: The Query Translator for EF Core 3 was changed and the query building
            //and evaluation is different. Now, when you try to order by a property name string then it crashes.
            //If an update fixes this or a workaround is found, then implement it into the OrderBy Extension
            //and uncomment the line below.

            return new PagedList<TEntity>(query, pageIndex, pageSize);
        }

        public async Task<IPagedList<TEntity>> GetPagedAsync(int pageIndex, int pageSize, string sortExpression = null)
        {
            return await Task.Run(() => GetPaged(pageIndex, pageSize, sortExpression));
        }

        public IPagedList<TEntity> GetPaged(Func<IQueryable<TEntity>, IQueryable<TEntity>> transform, Expression<Func<TEntity, bool>> filter = null, int pageIndex = -1, int pageSize = -1, string sortExpression = null)
        {
            var query = filter == null ? _DbSet.AsNoTracking() : _DbSet.AsNoTracking().Where(filter);

            var notSortedResults = transform(query);

            //Breaking Changes in EF Core 3: The Query Translator for EF Core 3 was changed and the query building
            //and evaluation is different. Now, when you try to order by a property name string then it crashes.
            //If an update fixes this or a workaround is found, then implement it into the OrderBy Extension
            //and uncomment the line below.

            return new PagedList<TEntity>(notSortedResults, pageIndex, pageSize);
        }

        public async Task<IPagedList<TEntity>> GetPagedAsync(Func<IQueryable<TEntity>, IQueryable<TEntity>> transform, Expression<Func<TEntity, bool>> filter = null, int pageIndex = -1, int pageSize = -1, string sortExpression = null)
        {
            return await Task.Run(() => this.GetPaged(transform, filter, pageIndex, pageSize, sortExpression));
        }

        public IPagedList<TResult> GetPaged<TResult>(Func<IQueryable<TEntity>, IQueryable<TResult>> transform, Expression<Func<TEntity, bool>> filter = null, int pageIndex = -1, int pageSize = -1, string sortExpression = null)
        {
            var query = filter == null ? _DbSet.AsNoTracking() : _DbSet.AsNoTracking().Where(filter);

            var notSortedResults = transform(query);

            //Breaking Changes in EF Core 3: The Query Translator for EF Core 3 was changed and the query building
            //and evaluation is different. Now, when you try to order by a property name string then it crashes.
            //If an update fixes this or a workaround is found, then implement it into the OrderBy Extension
            //and uncomment the line below.

            return new PagedList<TResult>(notSortedResults, pageIndex, pageSize);
        }

        public async Task<IPagedList<TResult>> GetPagedAsync<TResult>(Func<IQueryable<TEntity>, IQueryable<TResult>> transform, Expression<Func<TEntity, bool>> filter = null, int pageIndex = -1, int pageSize = -1, string sortExpression = null)
        {
            return await Task.Run(() => this.GetPaged(transform, filter, pageIndex, pageSize, sortExpression));
        }

        public int GetCount<TResult>(Func<IQueryable<TEntity>, IQueryable<TResult>> transform, Expression<Func<TEntity, bool>> filter = null)
        {
            var query = filter == null ? _DbSet.AsNoTracking() : _DbSet.AsNoTracking().Where(filter);

            return transform(query).Count();
        }

        public async Task<int> GetCountAsync<TResult>(Func<IQueryable<TEntity>, IQueryable<TResult>> transform, Expression<Func<TEntity, bool>> filter = null)
        {
            var query = filter == null ? _DbSet.AsNoTracking() : _DbSet.AsNoTracking().Where(filter);

            return await transform(query).CountAsync();
        }

        public TResult Get<TResult>(Func<IQueryable<TEntity>, IQueryable<TResult>> transform, Expression<Func<TEntity, bool>> filter = null, string sortExpression = null)
        {
            var query = filter == null ? _DbSet : _DbSet.Where(filter);

            var notSortedResults = transform(query);

            //Breaking Changes in EF Core 3: The Query Translator for EF Core 3 was changed and the query building
            //and evaluation is different. Now, when you try to order by a property name string then it crashes.
            //If an update fixes this or a workaround is found, then implement it into the OrderBy Extension
            //and uncomment the line below.

            return notSortedResults.FirstOrDefault();
        }

        public async Task<TResult> GetAsync<TResult>(Func<IQueryable<TEntity>, IQueryable<TResult>> transform, Expression<Func<TEntity, bool>> filter = null, string sortExpression = null)
        {
            TResult result = await Task.Run(async () =>
            {
                var query = filter == null ? _DbSet : _DbSet.Where(filter);

                var notSortedResults = transform(query);

                //Breaking Changes in EF Core 3: The Query Translator for EF Core 3 was changed and the query building
                //and evaluation is different. Now, when you try to order by a property name string then it crashes.
                //If an update fixes this or a workaround is found, then implement it into the OrderBy Extension
                //and uncomment the line below.
                return await notSortedResults.FirstOrDefaultAsync();
            });

            return result;
        }

        public bool Exists(Func<IQueryable<TEntity>, IQueryable<TEntity>> transform, Expression<Func<TEntity, bool>> filter = null)
        {
            var query = filter == null ? _DbSet.AsNoTracking() : _DbSet.AsNoTracking().Where(filter);

            var result = transform(query);

            return result.Any();
        }

        public async Task<bool> ExistsAsync(Func<IQueryable<TEntity>, IQueryable<TEntity>> transform, Expression<Func<TEntity, bool>> filter = null)
        {
            var query = filter == null ? _DbSet.AsNoTracking() : _DbSet.AsNoTracking().Where(filter);

            var result = transform(query);

            return await result.AnyAsync();
        }

        public bool Exists<TResult>(Func<IQueryable<TEntity>, IQueryable<TResult>> transform, Expression<Func<TEntity, bool>> filter = null)
        {
            var query = filter == null ? _DbSet.AsNoTracking() : _DbSet.AsNoTracking().Where(filter);

            var result = transform(query);

            return result.Any();
        }

        public async Task<bool> ExistsAsync<TResult>(Func<IQueryable<TEntity>, IQueryable<TResult>> transform, Expression<Func<TEntity, bool>> filter = null)
        {
            var query = filter == null ? _DbSet.AsNoTracking() : _DbSet.AsNoTracking().Where(filter);

            var result = transform(query);

            return await result.AnyAsync();
        }
    }
    
}
