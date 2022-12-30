using Ef.Datasource.Domain.Content;
using Ef.Persistence.Repositories;

namespace Domain.Interfaces.Content
{
    public interface IRepositoryPerson : IRepository<Person, int>
    {
        Task<int> AliveTotalAsync();
        Task<int> DeathTotalAsync();
    }
}
