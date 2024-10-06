using Core.DataAccess.Dynamic;
using Core.DataAccess.Paging;
using Core.Entites.Concrete.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Collections;
using System.Linq.Expressions;
using System.Reflection;

namespace Core.DataAccess.EntityFramework
{
    public class EfRepositoryBase<TEntity, TContext> : IAsyncRepository<TEntity>, IRepository<TEntity>
        where TEntity : BaseEntity
        where TContext : DbContext
    {
        private readonly TContext _context;

        public EfRepositoryBase(TContext context)
        {
            _context = context;
        }

        public DbSet<TEntity> Table => _context.Set<TEntity>();

        public async Task<bool> AddAsync(TEntity entity)
        {
            entity.CreatedDate = DateTime.UtcNow;
            EntityEntry<TEntity> entry = await Table.AddAsync(entity);
            return EntityState.Added == entry.State;
        }

        public async Task<bool> AddRangeAsync(List<TEntity> datas)
        {
            foreach (var item in datas)
                item.CreatedDate = DateTime.UtcNow;
            await Table.AddRangeAsync(datas);
            return true;
        }

        public async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> filter, bool tracking = true, bool withDeleted = false, CancellationToken cancellationToken = default)
        {
            var query = Table.AsQueryable();
            if (!tracking) query = query.AsNoTracking();
            if (withDeleted) query = query.IgnoreQueryFilters();
            return await query.FirstOrDefaultAsync(filter, cancellationToken);
        }

        public async Task<TEntity> RemoveAsync(TEntity entity, bool permanent = false)
        {
            await SetEntityAsDeletedAsync(entity, permanent);
            return entity;
        }

        public async Task<int> SaveAsync()
            => await _context.SaveChangesAsync();


        public bool Remove(TEntity entity)
        {
            EntityEntry<TEntity> entry = Table.Remove(entity);
            return entry.State == EntityState.Deleted;
        }
        public bool Update(TEntity entity)
        {
            entity.UpdatedDate = DateTime.UtcNow;
            EntityEntry<TEntity> entry = Table.Update(entity);
            return EntityState.Modified == entry.State;
        }

        public async Task<Paginate<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> filter = null, bool tracking = true, bool withDeleted = false, int index = 0, int size = 10, CancellationToken cancellationToken = default)
        {
            var query = Table.AsQueryable();
            if (!tracking) query = query.AsNoTracking();
            if (withDeleted) query = query.IgnoreQueryFilters();
            if (filter != null) query = query.Where(filter);
            return await query.ToPaginateAsync(index, size, cancellationToken);
        }

        public async Task<Paginate<TEntity>> GetAllByDynamicAsync(DynamicQuery dynamic, Expression<Func<TEntity, bool>> filter = null, bool tracking = true, bool withDeleted = false, int index = 0, int size = 10, CancellationToken cancellationToken = default)
        {
            var query = Table.AsQueryable().ToDynamic(dynamic);
            if (!tracking) query = query.AsNoTracking();
            if (withDeleted) query = query.IgnoreQueryFilters();
            if (filter != null) query = query.Where(filter);
            return await query.ToPaginateAsync(index, size, cancellationToken);
        }

        public bool Add(TEntity entity)
        {
            EntityEntry<TEntity> entry = Table.Add(entity);
            return EntityState.Added == entry.State;
        }

        public bool AddRange(List<TEntity> datas)
        {
            Table.AddRange(datas);
            return true;
        }

        public bool Delete(TEntity entity, bool permanent = false)
        {
            EntityEntry<TEntity> entry = Table.Remove(entity);
            return EntityState.Deleted == entry.State;
        }

        public int save()
            => _context.SaveChanges();

        public IQueryable<TEntity> GetAll(Expression<Func<TEntity, bool>> filter = null, bool tracking = true)
        {
            var query = Table.AsQueryable();
            if (!tracking) query = query.AsNoTracking();
            return filter == null ? query : query.Where(filter);
        }

        private async Task SetEntityAsDeletedAsync(TEntity entity, bool permanent)
        {
            if (!permanent)
            {
                CheckHasEntityHaveOneToOneRelation(entity);
                await SetEntityAsSoftDeletedAsync(entity);
            }
            else
            {
                Table.Remove(entity);
            }
        }

        private async Task SetEntityAsSoftDeletedAsync(IEntityTimestamps entity)
        {
            if (entity.DeletedDate.HasValue)
                return;
            entity.DeletedDate = DateTime.UtcNow;

            var navigations = _context.Entry(entity).Metadata.GetNavigations().Where(x => x is { IsOnDependent: false, ForeignKey.DeleteBehavior: DeleteBehavior.ClientCascade or DeleteBehavior.Cascade }).ToList();

            foreach (var navigation in navigations)
            {
                if (navigation.TargetEntityType.IsOwned()) continue;
                if (navigation.PropertyInfo == null) continue;

                var navValue = navigation.PropertyInfo.GetValue(entity);

                if (navigation.IsCollection)
                {
                    if (navValue == null)
                    {
                        IQueryable query = _context.Entry(entity).Collection(navigation.PropertyInfo.Name).Query();
                        navValue = await GetRealitionLoaderQuery(query).ToListAsync();                         //, navigation.PropertyInfo.GetType()

                        if (navValue == null) continue;
                    }
                    foreach (IEntityTimestamps navValueItem in (IEnumerable)navValue)
                        await SetEntityAsSoftDeletedAsync(navValueItem);
                }
                else
                {
                    if (navValue == null)
                    {
                        IQueryable query = _context.Entry(entity).Reference(navigation.PropertyInfo.Name).Query();
                        navValue = await GetRealitionLoaderQuery(query).FirstOrDefaultAsync();

                        if (navValue == null) continue;
                    }

                    await SetEntityAsSoftDeletedAsync((IEntityTimestamps)navValue);
                }
            }
            _context.Update(entity);
        }

        private IQueryable<object> GetRealitionLoaderQuery(IQueryable query)
        {
            Type queryProviderType = query.Provider.GetType();
            MethodInfo createQueryMethod = queryProviderType.GetMethods().First(x => x is { Name: nameof(query.Provider.CreateQuery), IsGenericMethod: true })?.MakeGenericMethod(queryProviderType) ??
                throw new InvalidOperationException("CreateQuery<TElement> method is not found in IQueryProvider");

            var queryProviderQuery = (IQueryable<object>)createQueryMethod.Invoke(query.Provider, new object[] { query.Expression })!;

            return queryProviderQuery.Where(x => !((IEntityTimestamps)x).DeletedDate.HasValue);
        }

        private void CheckHasEntityHaveOneToOneRelation(TEntity entity)
        {
            var hasEntityHaveOneToOneRelaiton = _context.Entry(entity).Metadata.GetForeignKeys().All(
                x => x.DependentToPrincipal?.IsCollection == true || x.PrincipalToDependent?.IsCollection == true || x.DependentToPrincipal.DeclaringEntityType.ClrType == GetType()
                ) == false;

            if (hasEntityHaveOneToOneRelaiton)
            {
                throw new InvalidOperationException("Entity has one-to-one relationship.Soft delete cauese problem if you try to create entry again by same foreign key");
            }
        }
    }
}
