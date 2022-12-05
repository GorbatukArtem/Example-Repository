namespace Persistence.Ef.Repositories
{
    public interface IRepository<TEntity, in TKey> where TEntity : class
    {
        TEntity Add(TEntity entity);
        void Update(TEntity entity);
        void Delete(TKey id);

        ValueTask<TEntity?> GetAsync(TKey id);
        ValueTask<TEntity?> GetAsync(TKey id, CancellationToken token);

        Task<List<TEntity>> GetAsync();
        Task<List<TEntity>> GetAsync(CancellationToken token);

        Task<int> CountAsync();
        Task<int> CountAsync(CancellationToken token);
    }
}
