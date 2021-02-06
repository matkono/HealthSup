using HealthSup.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HealthSup.Domain.Repositories
{
    public interface IPatientRepository
    {
        Task<PagedResult<List<Patient>>> ListPaged
        (
            int pageNumber,
            int pageSize
        );
    }
}
