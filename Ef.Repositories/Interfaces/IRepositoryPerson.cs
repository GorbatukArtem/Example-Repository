using Ef.Datasource.Domain.Content;
using Ef.Persistence.Repositories;

namespace Ef.Repositories.Interfaces
{
    public interface IRepositoryPerson : IRepository<Person, int>
    {
        Task<int> AliveTotalAsync();
        Task<int> DeathTotalAsync();
    }
}
