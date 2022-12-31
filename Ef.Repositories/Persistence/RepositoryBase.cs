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
            var entity = Context.Set<TEntity>().Find(id);

            ArgumentNullException.ThrowIfNull(entity);

            Context.Set<TEntity>().Remove(entity);
        }

        public virtual IQueryable<TEntity> GetAll()
        {
            return Context.Set<TEntity>().AsNoTracking();
        }

        public virtual TEntity? GetById(params TKey[] ids)
        {
            ArgumentNullException.ThrowIfNull(nameof(ids));

            return Context.Set<TEntity>().Find(new object?[] { ids });
        }
    }
}
