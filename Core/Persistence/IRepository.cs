namespace Core.Persistence;

public interface IRepository<TEntity,in TKey> where TEntity : class
{
    TEntity Create(TEntity entity);
    void Update(TEntity entity);
    void Delete(TKey id);

    Task<IEnumerable<TEntity>> GetAll(CancellationToken token = default);
    Task<TEntity?> GetById(CancellationToken token = default, params TKey[] ids);

    Task<int> SaveChangesAsync(CancellationToken token = default);
}
