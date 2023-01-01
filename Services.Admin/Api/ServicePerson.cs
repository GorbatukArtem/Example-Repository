using Core.Domain;
using Core.Interfaces;
using Services.Admin.Interfaces;
using Services.Admin.Models;

namespace Services.Admin.Api;

public class ServicePerson : IServicePerson
{
    public readonly IRepositoryPerson repositoryPerson;

    public ServicePerson(IRepositoryPerson repositoryPerson)
    {
        this.repositoryPerson = repositoryPerson;
    }

    public async Task Create(PersonCreate model, CancellationToken token = default)
    {
        ArgumentNullException.ThrowIfNull(model);

        var entity = new Person()
        {
            FirstName = model.FirstName,
            LastName = model.LastName,
            MiddleName= model.MiddleName,
            Birth = model.Birth,
            Death= model.Death,
        };

        repositoryPerson.Create(entity);

        await repositoryPerson.SaveChangesAsync(token);
    }

    public async Task Update(PersonUpdate model, CancellationToken token = default)
    {
        ArgumentNullException.ThrowIfNull(model);

        var entity = new Person()
        {
            Id= model.Id,
            FirstName = model.FirstName,
            LastName = model.LastName,
            MiddleName = model.MiddleName,
            Birth = model.Birth,
            Death = model.Death,
        };

        repositoryPerson.Update(entity);

        await repositoryPerson.SaveChangesAsync(token);
    }

    public async Task Delete(int? id, CancellationToken token = default)
    {
        ArgumentNullException.ThrowIfNull(id);

        repositoryPerson.Delete(id);

        await repositoryPerson.SaveChangesAsync(token);
    }

    public async Task<PersonalResult> GetAll(CancellationToken token = default)
    {
        var persons = await repositoryPerson.GetAll(token);

        var models = persons.Select(p => new PerosnalModel()
        {
            FirstName = p.FirstName,
            LastName = p.LastName,
            MiddleName = p.MiddleName,
            Birth = p.Birth,
            Death = p.Death,
        });

        return new PersonalResult() { Personals = models };
    }
}
