using Services.Admin.Models;

namespace Services.Admin.Interfaces
{
    public interface IServicePerson
    {
        Task Create(PersonCreate model, CancellationToken token = default);
        Task Update(PersonUpdate model, CancellationToken token = default);
        Task Delete(int? id, CancellationToken token = default);
        Task<PersonalResult> GetAll(CancellationToken token = default);
    }
}
