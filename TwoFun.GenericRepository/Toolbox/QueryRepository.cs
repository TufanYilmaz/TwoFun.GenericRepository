using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace TwoFun.GenericRepository.Toolbox
{
    public class QueryRepository<TDbContext> : IQueryRepository
        where TDbContext : DbContext
    {
        private readonly TDbContext _dbContext;
        public QueryRepository(TDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IQueryable<TEntity> GetQueryable<TEntity>()
            where TEntity : class => _dbContext.Set<TEntity>();

        public async Task<List<TEntity>> GetListAsync<TEntity>(
           Expression<Func<TEntity, bool>> condition,
           Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includes,
           bool asNoTracking,
           CancellationToken cancellationToken = default)
            where TEntity : class
        {
            var query = GetQueryable<TEntity>();

            if (condition != null)
            {
                query = query.Where(condition);
            }
            if (includes != null)
            {
                query = includes(query);
            }
            if (asNoTracking)
            {
                query = query.AsNoTracking();
            }
            List<TEntity> items = await query.ToListAsync(cancellationToken).ConfigureAwait(false);
            return items;
        }
        public Task<List<TEntity>> GetListAsync<TEntity>(
           Expression<Func<TEntity, bool>> condition,
           bool asNoTracking,
           CancellationToken cancellationToken = default)
            where TEntity : class
        {
            return GetListAsync(condition, null, asNoTracking, cancellationToken);
        }
        public Task<List<TEntity>> GetListAsync<TEntity>(
            Expression<Func<TEntity, bool>> condition, 
            CancellationToken cancellationToken = default)
            where TEntity : class
        {
            return GetListAsync(condition, false, cancellationToken);
        }
        public Task<List<TEntity>> GetListAsync<TEntity>(CancellationToken cancellationToken = default)
           where TEntity : class
        {
            return GetListAsync<TEntity>(false, cancellationToken);
        }

        public  Task<List<TEntity>> GetListAsync<TEntity>(bool asNoTracking, CancellationToken cancellationToken = default)
            where TEntity : class
        {
            Expression<Func<TEntity, bool>> conditionNull = null;
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includeNull = null;
            return GetListAsync(condition: conditionNull, includes: includeNull, asNoTracking: asNoTracking,cancellationToken: cancellationToken);
        }
        public Task<List<T>> GetListAsync<T>(
            Func<IQueryable<T>, IIncludableQueryable<T, object>> includes,
            CancellationToken cancellationToken = default)
            where T : class
        {
            return GetListAsync(includes, false, cancellationToken);
        }
        public async Task<List<TEntity>> GetListAsync<TEntity>(
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includes,
            bool asNoTracking,
            CancellationToken cancellationToken = default)
            where TEntity : class
        {
            IQueryable<TEntity> query = _dbContext.Set<TEntity>();
            if (includes != null)
            {
                query = includes(query);
            }
            if (asNoTracking)
            {
                query = query.AsNoTracking();
            }
            List<TEntity> items = await query.ToListAsync(cancellationToken).ConfigureAwait(false);
            return items;
        }

        public async Task<List<TProjectedType>> GetListAsync<TEntity, TProjectedType>(
            Expression<Func<TEntity, TProjectedType>> selectExpression,
            CancellationToken cancellationToken = default) where TEntity : class
        {
            if (selectExpression == null)
            {
                throw new ArgumentNullException(nameof(selectExpression));
            }

            List<TProjectedType> entities = await _dbContext.Set<TEntity>()
                .Select(selectExpression).ToListAsync(cancellationToken).ConfigureAwait(false);

            return entities;
        }

        public async Task<List<TProjectedType>> GetListAsync<TEntity, TProjectedType>(
            Expression<Func<TEntity, bool>> condition,
            Expression<Func<TEntity, TProjectedType>> selectExpression, 
            CancellationToken cancellationToken = default) where TEntity : class
        {
            if (selectExpression == null)
            {
                throw new ArgumentNullException(nameof(selectExpression));
            }

            IQueryable<TEntity> query = _dbContext.Set<TEntity>();

            if (condition != null)
            {
                query = query.Where(condition);
            }

            List<TProjectedType> projectedEntites = await query.Select(selectExpression)
                .ToListAsync(cancellationToken).ConfigureAwait(false);

            return projectedEntites;
        }
    }
}
