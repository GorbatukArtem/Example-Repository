namespace Core.Persistence
{
    public interface IRepository<TEntity, in TKey> where TEntity : class
    {
        TEntity Create(TEntity entity);
        void Update(TEntity entity);
        void Delete(TKey id);

        IQueryable<TEntity> GetAll();
        TEntity? GetById(params TKey[] ids);
    }
}
