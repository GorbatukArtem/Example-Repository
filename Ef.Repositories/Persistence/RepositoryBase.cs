using Core.Persistence;
using Ef.Datasource.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Ef.Repositories.Persistence
{
    public abstract class RepositoryBase<TEntity, TKey> : IRepository<TEntity, TKey> where TEntity : class
    {
        protected readonly DbContextPersonalCard Context;

        protected RepositoryBase(DbContextPersonalCard context)
        {
            Context = context;
        }

        public TEntity Create(TEntity entity)
        {
            ArgumentNullException.ThrowIfNull(entity);

            Context.Set<TEntity>().Add(entity);

            return entity;
        }

        public void Update(TEntity entity)
        {
            ArgumentNullException.ThrowIfNull(entity);

            if (Context.Entry(entity).State == EntityState.Detached)
                Context.Set<TEntity>().Attach(entity);

            Context.Set<TEntity>().Update(entity);
        }

        public void Delete(TKey id)
        {
            var entity = Context.Set<TEntity>().Find(id);

            ArgumentNullException.ThrowIfNull(entity);

            Context.Set<TEntity>().Remove(entity);
        }

        public ValueTask<TEntity?> GetAsync(TKey id, CancellationToken token = default)
        {
            ArgumentNullException.ThrowIfNull(nameof(id));

            return Context.Set<TEntity>().FindAsync(new object?[] { new TKey?[] { id }, token }, token);
        }

        public IQueryable<TEntity> GetAsync(CancellationToken token = default)
        {
            return Context.Set<TEntity>().AsNoTracking();
        }

        public Task<int> CountAsync(CancellationToken token = default)
        {
            return Context.Set<TEntity>().CountAsync(token);
        }
    }
}
