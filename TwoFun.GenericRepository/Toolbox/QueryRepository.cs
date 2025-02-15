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
    }
}
