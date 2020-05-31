using Cardiompp.Application.DataContracts.Responses;
using Cardiompp.Application.DataContracts.v1.Requests.Doctor;
using Cardiompp.Application.DataContracts.v1.Responses.Doctor;
using System.Threading.Tasks;

namespace Cardiompp.Application.Services.Contracts
{
    public interface IDoctorApplicationService
    {
        Task<DoctorResponse<GetDoctorByEmailAndPasswordResponse>> GetByEmailAndPassword(GetDoctorByEmailAndPasswordRequest loginRequest);

        Task<BaseResponse> UpdatePassword(UpdatePasswordRequest updatePasswordRequest);
    }
}
