using Microsoft.EntityFrameworkCore.Storage;

namespace TwoFun.GenericRepository
{
    public interface IRepository
    {
       
        Task InsertAsync<TEntity>(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
            where TEntity : class;
      
        Task<object?[]> InsertAsync<TEntity>(TEntity entity, CancellationToken cancellationToken = default)
            where TEntity : class;
        
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

        void Add<TEntity>(TEntity entity)
            where TEntity : class;
        
        Task AddAsync<TEntity>(TEntity entity, CancellationToken cancellationToken = default)
            where TEntity : class;
       
        void Add<TEntity>(IEnumerable<TEntity> entities)
            where TEntity : class;
        
        Task AddAsync<TEntity>(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
            where TEntity : class;
        
        void Remove<TEntity>(TEntity entity)
            where TEntity : class;

        void Remove<TEntity>(IEnumerable<TEntity> entities)
            where TEntity : class;
        Task<IDbContextTransaction> BeginTransactionAsync(
            CancellationToken cancellationToken = default);
        Task RollbackTransactionAsync(
          IDbContextTransaction dbContextTransaction,
          CancellationToken cancellationToken = default);

        Task CommitTransactionAsync(
          IDbContextTransaction dbContextTransaction,
          CancellationToken cancellationToken = default);

    }
}
