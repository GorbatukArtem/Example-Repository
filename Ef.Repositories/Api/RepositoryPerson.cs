using Ef.Datasource.Contexts;
using Ef.Datasource.Domain.Content;
using Ef.Persistence.Repositories;
using Ef.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Ef.Repositories.Api
{
    public class RepositoryPerson : RepositoryBase<Person, int>, IRepositoryPerson
    {
        public RepositoryPerson(DbContextPersonalCard context) : base(context) { }

        public Task<int> AliveTotalAsync()
        {
            return Context.Persons
                .CountAsync(p => p.Death == null && p.Birth != null && p.Birth.Value.Year > DateTime.Now.Year - 120);
        }

        public Task<int> DeathTotalAsync()
        {
            return Context.Persons
                .CountAsync(p => p.Birth != null && p.Death != null);
        }
    }
}
