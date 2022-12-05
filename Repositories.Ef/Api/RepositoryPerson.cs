using Datasource.Ef.Contexts;
using Datasource.Ef.Domain.Content;
using Microsoft.EntityFrameworkCore;
using Persistence.Ef.Repositories;
using Repositories.Ef.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Ef.Api
{
    public class RepositoryPerson : RepositoryBase<Person, int>, IRepositoryPerson
    {
        public RepositoryPerson(DbContextPersonalCard context) : base(context) { }

        public Task<int> AliveTotalAsync()
        {
            return Context.Persons
                .CountAsync(p => p.Death == null && p.Birth != null && p.Birth.Value.Year > DateTime.Now.Year - 120);
        }
    }
}
