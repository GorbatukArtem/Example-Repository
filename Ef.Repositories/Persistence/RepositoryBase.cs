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

        public virtual TEntity Create(TEntity entity)
        {
            ArgumentNullException.ThrowIfNull(entity);

            Context.Set<TEntity>().Add(entity);

            return entity;
        }

        public virtual void Update(TEntity entity)
        {
            ArgumentNullException.ThrowIfNull(entity);

            if (Context.Entry(entity).State == EntityState.Detached)
                Context.Set<TEntity>().Attach(entity);

            Context.Set<TEntity>().Update(entity);
        }

        public virtual void Delete(TKey id)
        {
            ArgumentNullException.ThrowIfNull(id);

            var entity = Context.Set<TEntity>().Find(id);

            ArgumentNullException.ThrowIfNull(entity);

            Context.Set<TEntity>().Remove(entity);
        }

        public virtual async Task<IEnumerable<TEntity>> GetAll(CancellationToken token = default)
        {
            return await Context.Set<TEntity>().AsNoTracking().ToListAsync(token);
        }

        public virtual async Task<TEntity?> GetById(CancellationToken token = default, params TKey[] ids)
        {
            ArgumentNullException.ThrowIfNull(nameof(ids));

            return await Context.Set<TEntity>().FindAsync(new object?[] { ids }, token);
        }

        /// <summary>
        /// EF DbContext поступает через конструктор через внедрение зависимостей.
        /// Он совместно используется несколькими репозиториями в одной и той же области HTTP-запроса
        /// благодаря его времени жизни по умолчанию - ServiceLifetime.Scoped в контейнере IoC
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public Task<int> SaveChangesAsync(CancellationToken token = default)
        {
            return Context.SaveChangesAsync(token);
        }
    }
}
