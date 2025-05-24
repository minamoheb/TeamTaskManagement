using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System.Data;
using System.Linq.Expressions;

namespace TeamTaskManagement.Infrastructure.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly DbContext _dbContext;
        private readonly DbSet<T> _dbSet;

        public Repository(DbContext context)
        {
            _dbContext = context ?? throw new ArgumentException(nameof(context));
            _dbSet = _dbContext.Set<T>();
        }


        public async Task<T> Find(object id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<T> Insert(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            await _dbContext.Set<T>().AddAsync(entity);

            return entity;
        }
        public async Task<List<T>> InsertRange(List<T> entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            await _dbContext.Set<T>().AddRangeAsync(entity);
            //await _dbContext.SaveChangesAsync();
            return entity;
        }
        public async Task<T> Update(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            _dbContext.Set<T>().Update(entity);
            //await _dbContext.SaveChangesAsync();
            return entity;

        }

        public async Task<List<T>> UpdateRange(List<T> entities)
        {
            if (entities == null)
            {
                throw new ArgumentNullException(nameof(entities));
            }

            _dbContext.Set<T>().UpdateRange(entities);

            return entities;
        }
        public async Task<T> Delete(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            _dbContext.Set<T>().Remove(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }
        public async Task<List<T>> DeleteRange(List<T> entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            _dbContext.Set<T>().RemoveRange(entity);
            return entity;
        }
        public async Task<List<T>> GetList(Expression<Func<T, bool>> predicate,
                Func<IQueryable<T>, IOrderedQueryable<T>> orderBy,
                Func<IQueryable<T>, IIncludableQueryable<T, object>> include,
                bool disableTracking)
        {
            IQueryable<T> query = _dbSet;

            if (disableTracking) query = query.AsNoTracking();

            if (include != null) query = include(query).AsSplitQuery();

            if (predicate != null) query = query.Where(predicate).AsSplitQuery();

            if (orderBy != null)
                return await orderBy(query.AsSplitQuery()).ToListAsync();
            return await query.AsSplitQuery().ToListAsync();
        }
        public async Task<List<TResult>> GetSelectedPropertiesList<TResult>(
            Expression<Func<T, bool>> predicate,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy,
            Func<IQueryable<T>, IIncludableQueryable<T, object>> include,
             Expression<Func<T, TResult>> selector,
            bool disableTracking)
        {
            IQueryable<T> query = _dbSet;

            if (disableTracking) query = query.AsNoTracking();

            if (include != null) query = include(query);

            if (predicate != null) query = query.Where(predicate);

            if (orderBy != null)
            {
                query = orderBy(query); // Apply ordering to the query
            }

            IQueryable<TResult> resultQuery = query.Select(selector);

            return await resultQuery.ToListAsync();
        }

        public List<T> GetListNotAsync(Expression<Func<T, bool>> predicate,
        Func<IQueryable<T>, IOrderedQueryable<T>> orderBy,
        Func<IQueryable<T>, IIncludableQueryable<T, object>> include,
        bool disableTracking)
        {
            IQueryable<T> query = _dbSet;

            if (disableTracking) query = query.AsNoTracking();

            if (include != null) query = include(query);

            if (predicate != null) query = query.Where(predicate);

            return orderBy != null ? orderBy(query).ToList() : query.ToList();
        }

        public async Task<T> GetbyFirstordefault(Expression<Func<T, bool>> predicate,
          Func<IQueryable<T>, IOrderedQueryable<T>> orderBy,
          Func<IQueryable<T>, IIncludableQueryable<T, object>> include,
          bool disableTracking)
        {
            IQueryable<T> query = _dbSet;

            if (disableTracking) query = query.AsNoTracking();

            if (include != null) query = include(query).AsSplitQuery();

            if (predicate != null) query = query.Where(predicate).AsSplitQuery();

            return orderBy != null ? orderBy(query.AsSplitQuery()).FirstOrDefault() : query.AsSplitQuery().FirstOrDefault();
        }


        public async Task<int> CountAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.CountAsync(predicate);
        }
        public void Dispose()
        {
            _dbContext?.Dispose();
        }
    }
}
