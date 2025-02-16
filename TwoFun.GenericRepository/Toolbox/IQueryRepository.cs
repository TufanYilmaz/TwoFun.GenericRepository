using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace TwoFun.GenericRepository.Toolbox
{
    public interface IQueryRepository
    {
        IQueryable<TEntity> GetQueryable<TEntity>()
           where TEntity : class;
        Task<List<T>> GetListAsync<T>(
           Expression<Func<T, bool>> condition,
           Func<IQueryable<T>, IIncludableQueryable<T, object>> includes,
           bool asNoTracking,
           CancellationToken cancellationToken = default)
            where T : class;
        Task<List<TEntity>> GetListAsync<TEntity>(
           Expression<Func<TEntity, bool>> condition,
           bool asNoTracking,
           CancellationToken cancellationToken = default)
            where TEntity : class;
        Task<List<TEntity>> GetListAsync<TEntity>(
            Expression<Func<TEntity, bool>> condition,
            CancellationToken cancellationToken = default)
            where TEntity : class;

    }
}
