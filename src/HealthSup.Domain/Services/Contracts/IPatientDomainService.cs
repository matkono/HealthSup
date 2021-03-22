using HealthSup.Domain.Entities;
using System.Threading.Tasks;

namespace HealthSup.Domain.Services.Contracts
{
    public interface IPatientDomainService
    {
        Task<Patient> Create
        (
            Patient patient
        );
    }
}
