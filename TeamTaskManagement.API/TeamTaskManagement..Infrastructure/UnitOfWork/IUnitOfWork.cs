using Microsoft.EntityFrameworkCore;
using TeamTaskManagement.Infrastructure.Repository;

namespace TeamTaskManagement.Infrastructure.UnitOfWork
{
    public interface IUnitOfWork<TContext> where TContext : DbContext
    {
        IRepository<TEntity> GetRepository<TEntity>() where TEntity : class;
        Task<int> DoWork();
        DbContext GetDbContext();
    }
}
