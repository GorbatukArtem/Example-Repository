using Repositories.Ef.Interfaces;
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

        public Task<HomeResult> GetAsync()
        {
            var personsTotal = repositoryPerson.CountAsync();

            var aliveTotal = repositoryPerson.AliveTotalAsync();

            var deadTotal = repositoryPerson.DeathTotalAsync();

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
