using Core.Domain.Content;
using Core.Interfaces.Content;
using Ef.Datasource.Contexts;
using Ef.Repositories.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Ef.Repositories.Api
{
    public class RepositoryPerson : RepositoryBase<Person, int>, IRepositoryPerson
    {
        public RepositoryPerson(DbContextPersonalCard context) : base(context) { }

        public Task<int> AliveTotalAsync()
        {
            return Context.Set<Person>()
                .CountAsync(p => p.Death == null && p.Birth != null && p.Birth.Value.Year > DateTime.Now.Year - 120);
        }

        public Task<int> DeathTotalAsync()
        {
            return Context.Set<Person>()
                .CountAsync(p => p.Birth != null && p.Death != null);
        }
    }
}
