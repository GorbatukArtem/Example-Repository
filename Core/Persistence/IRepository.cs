namespace Core.Persistence
{
    public interface IRepository<TEntity, in TKey> where TEntity : class
    {
        TEntity Create(TEntity entity);
        void Update(TEntity entity);
        void Delete(TKey id);

        ValueTask<TEntity?> GetAsync(TKey id, CancellationToken token = default);
        //Task<IQueryable<TEntity>> GetAsync(CancellationToken token = default);
        IQueryable<TEntity> GetAsync(CancellationToken token = default);

        Task<int> CountAsync(CancellationToken token = default);
    }
}
