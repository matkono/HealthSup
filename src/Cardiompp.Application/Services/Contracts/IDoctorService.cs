using Cardiompp.Application.DataContracts.v1.Responses.Doctor;
using System.Threading.Tasks;

namespace Cardiompp.Application.Services.Contracts
{
    public interface IDoctorService
    {
        Task<GetDoctorResponse> GetByAffiliationCodeAsync(string crm);
    }
}
