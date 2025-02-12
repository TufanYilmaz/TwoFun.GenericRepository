using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

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

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            int count = await _dbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
            return count;
        }
    }
}
