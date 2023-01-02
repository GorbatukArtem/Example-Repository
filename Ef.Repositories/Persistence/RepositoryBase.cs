using Core.Persistence;
using Ef.Datasource.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Ef.Repositories.Persistence;

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

        Context.Add(entity);

        return entity;
    }

    public virtual void Update(TEntity entity)
    {
        ArgumentNullException.ThrowIfNull(entity);

        // solution related to disconnected id

        var keyValues = Context.Model
            .FindEntityType(typeof(TEntity))?
            .FindPrimaryKey()?
            .Properties
            .Select(p => typeof(TEntity).GetProperty(p.Name))
            .Select(p => p?.GetValue(entity))
            .ToArray();

        var connectedEntity = Context.Find<TEntity>(keyValues);

        if (connectedEntity == null)
        {
            if (Context.Entry(entity).State == EntityState.Detached)
            {
                Context.Attach(entity);
            }

            Context.Update(entity);
        }
        else
        {
            //Context.ChangeTracker.TrackGraph(connectedEntity, p =>
            //{
            //    if (p.Entry.IsKeySet)
            //    {
            //        p.Entry.State = EntityState.Unchanged;
            //    }
            //});

            Context.Entry(connectedEntity).CurrentValues.SetValues(entity);
        }
    }

    public virtual void Delete(TKey id)
    {
        ArgumentNullException.ThrowIfNull(id);

        var entity = Context.Set<TEntity>().Find(id);

        ArgumentNullException.ThrowIfNull(entity);

        Context.Remove(entity);
    }

    public virtual async Task<IEnumerable<TEntity>> GetAll(CancellationToken token = default)
    {
        return await Context.Set<TEntity>().AsNoTracking().ToListAsync(token);
    }

    public virtual async Task<TEntity?> GetById(CancellationToken token = default, params TKey[] ids)
    {
        ArgumentNullException.ThrowIfNull(nameof(ids));

        return await Context.FindAsync<TEntity>(new object?[] { ids }, token);
    }

    /// <summary>
    /// EF DbContext поступает через конструктор через внедрение зависимостей.
    /// Он совместно используется несколькими репозиториями в одной и той же области HTTP-запроса
    /// благодаря его времени жизни по умолчанию - ServiceLifetime.Scoped в контейнере IoC
    /// </summary>
    /// <param name="token"></param>
    /// <returns></returns>
    public virtual Task<int> SaveChangesAsync(CancellationToken token = default)
    {
        return Context.SaveChangesAsync(token);
    }
}
