using HealthSup.Application.DataContracts.v1.Requests.Patient;
using HealthSup.Application.DataContracts.v1.Responses.Patient;
using System.Threading.Tasks;

namespace HealthSup.Application.Services.Contracts
{
    public interface IPatientApplicationService
    {
        public Task<ListPatientsPagedReturn> ListPaged
        (
            ListPatientPagedRequest argument
        );

        public Task<GetPatientByRegistrationReturn> GetByRegistration
        (
            string registration
        );
    }
}
