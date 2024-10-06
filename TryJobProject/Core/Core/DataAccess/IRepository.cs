using Core.Entites.Concrete.Common;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Core.DataAccess
{
    public interface IRepository<TEntity> where TEntity : BaseEntity
    {
        DbSet<TEntity> Table { get; }
        IQueryable<TEntity> GetAll(Expression<Func<TEntity, bool>> filter = null, bool tracking = true);
        bool Add(TEntity entity);
        bool AddRange(List<TEntity> datas);
        bool Delete(TEntity entity,bool permanent);
        bool Update(TEntity entity);
        int save();

    }
}
