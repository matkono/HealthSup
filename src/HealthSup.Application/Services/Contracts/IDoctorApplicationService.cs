using HealthSup.Application.DataContracts.v1.Responses.Doctor;
using System.Threading.Tasks;

namespace HealthSup.Application.Services.Contracts
{
    public interface IDoctorApplicationService
    {
        public Task<GetDoctorByUserIdReturn> GetByUserId
        (
            int userId
        );
    }
}
