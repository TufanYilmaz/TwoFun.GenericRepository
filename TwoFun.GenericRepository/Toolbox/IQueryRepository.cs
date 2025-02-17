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
        Task<List<TEntity>> GetListAsync<TEntity>(
           Expression<Func<TEntity, bool>> condition,
           Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includes,
           bool asNoTracking,
           CancellationToken cancellationToken = default)
            where TEntity : class;
        Task<List<TEntity>> GetListAsync<TEntity>(
           Expression<Func<TEntity, bool>> condition,
           bool asNoTracking,
           CancellationToken cancellationToken = default)
            where TEntity : class;
        Task<List<TEntity>> GetListAsync<TEntity>(
            Expression<Func<TEntity, bool>> condition,
            CancellationToken cancellationToken = default)
            where TEntity : class;
        Task<List<TEntity>> GetListAsync<TEntity>(
         Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includes,
         bool asNoTracking,
         CancellationToken cancellationToken = default)
         where TEntity : class;
        Task<List<T>> GetListAsync<T>(
            Func<IQueryable<T>, IIncludableQueryable<T, object>> includes,
            CancellationToken cancellationToken = default)
            where T : class;
        public Task<List<TEntity>> GetListAsync<TEntity>(bool asNoTracking, CancellationToken cancellationToken = default)
           where TEntity : class;
        public Task<List<TEntity>> GetListAsync<TEntity>(CancellationToken cancellationToken = default)
           where TEntity : class;

    }
}
