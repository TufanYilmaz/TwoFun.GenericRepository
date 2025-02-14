using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage;
using System.Data;

namespace TwoFun.GenericRepository
{
    internal sealed class Repository<TDbContext> : IRepository
        where TDbContext : DbContext
        //where TEntity : class
    {
        private readonly TDbContext _dbContext;
        public Repository(TDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Add<TEntity>(TEntity entity)
            where TEntity : class
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            _dbContext.Set<TEntity>().Add(entity);
        }

        public void Add<TEntity>(IEnumerable<TEntity> entities) where TEntity : class
        {
            if (entities == null)
            {
                throw new ArgumentNullException(nameof(entities));
            }
            _dbContext.Set<TEntity>().AddRange(entities);
        }

        public async Task AddAsync<TEntity>(TEntity entity, CancellationToken cancellationToken = default)
   where TEntity : class
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            await _dbContext.Set<TEntity>().AddAsync(entity, cancellationToken).ConfigureAwait(false);
        }

        public async Task AddAsync<TEntity>(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default) where TEntity : class
        {
            if (entities == null)
            {
                throw new ArgumentNullException(nameof(entities));
            }
            await _dbContext.Set<TEntity>().AddRangeAsync(entities, cancellationToken).ConfigureAwait(false);
        }

        public async Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default)
        {
            IDbContextTransaction dbContextTransaction = await _dbContext.Database.BeginTransactionAsync(cancellationToken);
            return dbContextTransaction;
        }

        public async Task CommitTransactionAsync(IDbContextTransaction dbContextTransaction, CancellationToken cancellationToken = default)
        {
            await dbContextTransaction.CommitAsync(cancellationToken);
        }

        public async Task InsertAsync<TEntity>(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default) where TEntity : class
        {
            if(entities == null)
            {
                throw new ArgumentNullException(nameof(TEntity) + " " + nameof(entities));
            }
            await _dbContext.Set<TEntity>().AddRangeAsync(entities, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

        }

        public async Task<object?[]> InsertAsync<TEntity>(TEntity entity, CancellationToken cancellationToken = default) where TEntity : class
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            EntityEntry<TEntity> entityEntry = await _dbContext.Set<TEntity>().AddAsync(entity, cancellationToken).ConfigureAwait(false);
            await _dbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

            Microsoft.EntityFrameworkCore.Metadata.IEntityType metadata = entityEntry.Metadata;
            IReadOnlyList<Microsoft.EntityFrameworkCore.Metadata.IProperty>? properties = metadata.FindPrimaryKey()?.Properties;
            object?[] primaryKeyValue = properties?.Select(p => entityEntry.Property(p.Name).CurrentValue).ToArray() ?? [];
            return primaryKeyValue;
        }

        public void Remove<TEntity>(TEntity entity) where TEntity : class
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
            _dbContext.Remove(entity);
        }

        public void Remove<TEntity>(IEnumerable<TEntity> entities) where TEntity : class
        {
            if (entities == null)
            {
                throw new ArgumentNullException(nameof(entities));
            }
            _dbContext.RemoveRange(entities);
        }

        public async Task RollbackTransactionAsync(IDbContextTransaction dbContextTransaction, CancellationToken cancellationToken = default)
        {
            await dbContextTransaction.RollbackAsync(cancellationToken);
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            int count = await _dbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
            return count;
        }


    }
}
