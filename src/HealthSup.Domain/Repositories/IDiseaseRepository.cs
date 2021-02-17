using HealthSup.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HealthSup.Domain.Repositories
{
    public interface IDiseaseRepository
    {
        Task<PagedResult<List<Disease>>> ListPaged
        (
            int pageNumber,
            int pageSize
        );

        Task<Disease> GetById
        (
            int id
        );
    }
}
