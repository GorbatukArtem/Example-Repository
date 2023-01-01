using Core.Domain;
using Core.Persistence;

namespace Core.Interfaces;

public interface IRepositoryPerson : IRepository<Person, int?>
{
    Task<int> TotalAsync(CancellationToken token = default);
    Task<int> AliveTotalAsync(CancellationToken token = default);
    Task<int> DeathTotalAsync(CancellationToken token = default);
}
