using Datasource.Ef.Contexts;

namespace Persistence.Ef.Repositories
{
    public abstract class RepositoryBase<TEntity, TKey> : IRepository<TEntity, TKey> where TEntity : class
    {
        protected readonly DbContextPersonalCard Context;

        protected RepositoryBase(DbContextPersonalCard context)
        {
            Context = context;
        }

        public TEntity Add(TEntity entity)
        {
            ArgumentNullException.ThrowIfNull(nameof(entity));

            Context.Set<TEntity>().Add(entity);

            return entity;
        }

        public void Update(TEntity entity)
        {
            ArgumentNullException.ThrowIfNull(nameof(entity));

            if (Context.Entry(entity).State == EntityState.Detached)
                Context.Set<TEntity>().Attach(entity);

            Context.Set<TEntity>().Update(entity);
        }

        public void Delete(TKey id)
        {
            var entity = Context.Set<TEntity>().Find(id);

            ArgumentNullException.ThrowIfNull(nameof(entity));

            Context.Set<TEntity>().Remove(entity);
        }


        public ValueTask<TEntity?> GetAsync(TKey id)
        {
            return GetAsync(id, CancellationToken.None);
        }
        public ValueTask<TEntity?> GetAsync(TKey id, CancellationToken token)
        {
            ArgumentNullException.ThrowIfNull(nameof(id));

            return Context.Set<TEntity>().FindAsync(new object?[] { id }, token);
        }

        public Task<List<TEntity>> GetAsync()
        {
            return GetAsync(CancellationToken.None);
        }
        public Task<List<TEntity>> GetAsync(CancellationToken token)
        {
            return Context.Set<TEntity>().AsNoTracking().ToListAsync(token);
        }

        public Task<int> CountAsync()
        {
            return CountAsync(CancellationToken.None);
        }
        public Task<int> CountAsync(CancellationToken token)
        {
            return Context.Set<TEntity>().CountAsync(token);
        }
    }
}
