using Datasource.Ef.Domain.Content;
using Persistence.Ef.Repositories;

namespace Repositories.Ef.Interfaces
{
    public interface IRepositoryPerson : IRepository<Person, int>
    {
        Task<int> AliveTotalAsync();
        Task<int> DeathTotalAsync();
    }
}
