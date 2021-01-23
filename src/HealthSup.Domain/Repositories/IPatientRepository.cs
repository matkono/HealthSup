using HealthSup.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HealthSup.Domain.Repositories
{
    public interface IPatientRepository
    {
        Task<List<Patient>> ListPaged
        (
            uint pageNumber,
            uint pageSize
        );
    }
}
