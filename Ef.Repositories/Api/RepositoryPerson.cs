using Core.Domain;
using Core.Interfaces;
using Ef.Datasource.Contexts;
using Ef.Repositories.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Ef.Repositories.Api;

public class RepositoryPerson : RepositoryBase<Person, int>, IRepositoryPerson
{
    public RepositoryPerson(DbContextPersonalCard context) : base(context) { }

    public Task<int> TotalAsync(CancellationToken token = default)
    {
        return Context.Set<Person>().CountAsync(token);
    }

    public Task<int> AliveTotalAsync(CancellationToken token = default)
    {
        return Context.Set<Person>()
            .CountAsync(p => p.Death == null && p.Birth != null && p.Birth.Value.Year + 125 >= DateTime.Now.Year, token);
    }

    public Task<int> DeathTotalAsync(CancellationToken token = default)
    {
        return Context.Set<Person>()
            .CountAsync(p => p.Death != null || (p.Birth != null && p.Birth.Value.Year + 125 < DateTime.Now.Year), token);
    }
}
