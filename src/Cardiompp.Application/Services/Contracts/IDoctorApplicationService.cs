using Cardiompp.Application.DataContracts.Responses;
using Cardiompp.Application.DataContracts.v1.Requests.Doctor;
using System.Threading.Tasks;

namespace Cardiompp.Application.Services.Contracts
{
    public interface IDoctorApplicationService
    {
        Task<BaseResponse> UpdatePassword(UpdatePasswordRequest updatePasswordRequest);
    }
}
