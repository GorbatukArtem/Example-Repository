using Core.Interfaces.Content;
using Microsoft.EntityFrameworkCore;
using Services.Content.Domain;
using Services.Content.Interfaces;

namespace Services.Content.Api
{
    public class ServiceHome : IServiceHome
    {
        private readonly IRepositoryPerson repositoryPerson;

        public ServiceHome(IRepositoryPerson repositoryPerson)
        {
            this.repositoryPerson = repositoryPerson;
        }

        public Task<HomeResult> GetAsync(CancellationToken token = default)
        {
            var personsTotal = repositoryPerson.TotalAsync(token);

            var aliveTotal = repositoryPerson.AliveTotalAsync(token);

            var deadTotal = repositoryPerson.DeathTotalAsync(token);

            Task.WaitAll(personsTotal, aliveTotal, deadTotal);

            return Task.FromResult(new HomeResult()
            {
                PersonsTotal = personsTotal.Result,
                AliveTotal = aliveTotal.Result,
                DeadTotal = deadTotal.Result,
            });
        }
    }
}
