using Cardiompp.Application.DataContracts.v1.Requests.Doctor;
using Cardiompp.Application.DataContracts.v1.Responses.Doctor;
using System.Threading.Tasks;

namespace Cardiompp.Application.Services.Contracts
{
    public interface IDoctorService
    {
        Task<GetDoctorResponse<GetDoctorByCrmResponse>> GetByCrm(string crm);

        Task<GetDoctorResponse<GetDoctorByEmailAndPasswordResponse>> GetByEmailAndPassword(GetDoctorByEmailAndPasswordRequest loginRequest);
    }
}
