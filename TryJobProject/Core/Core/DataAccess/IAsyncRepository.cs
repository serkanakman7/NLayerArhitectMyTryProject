using Core.DataAccess.Dynamic;
using Core.DataAccess.Paging;
using Core.Entites.Concrete.Common;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Core.DataAccess
{
    public interface IAsyncRepository<TEntity> where TEntity : BaseEntity
    {
        DbSet<TEntity> Table { get; }
        Task<Paginate<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> filter = null, bool tracking = true, bool withDeleted = false, int index = 0, int size = 10, CancellationToken cancellationToken = default);
        Task<Paginate<TEntity>> GetAllByDynamicAsync(DynamicQuery dynamicQuery,Expression<Func<TEntity, bool>> filter = null, bool tracking = true, bool withDeleted = false, int index = 0, int size = 10, CancellationToken cancellationToken = default);
        Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> filter, bool tracking = true, bool withDeleted = false, CancellationToken cancellationToken = default);
        Task<bool> AddAsync(TEntity entity);
        Task<bool> AddRangeAsync(List<TEntity> enitites);
        Task<TEntity> RemoveAsync(TEntity entity, bool permanent = false);
        Task<int> SaveAsync();
    }
}
