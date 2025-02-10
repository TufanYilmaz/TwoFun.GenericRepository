using Microsoft.EntityFrameworkCore;

namespace TwoFun.GenericRepository
{
    public class Repository<TDbContext> : IRepository
        where TDbContext : DbContext
    {
        private readonly TDbContext _dbContext;
        public Repository(TDbContext dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
