using Core.Domain.Content;
using Core.Persistence;

namespace Core.Interfaces.Content
{
    public interface IRepositoryPerson : IRepository<Person, int>
    {
        Task<int> AliveTotalAsync();
        Task<int> DeathTotalAsync();
    }
}
