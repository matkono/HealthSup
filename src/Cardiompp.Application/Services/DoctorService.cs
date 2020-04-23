using Cardiompp.Application.DataContracts.v1.Responses.Doctor;
using Cardiompp.Application.Services.Contracts;
using System.Threading.Tasks;

namespace Cardiompp.Application.Services
{
    public class DoctorService : IDoctorService
    {
        public Task<GetDoctorResponse> GetByAffiliationCodeAsync(string crm)
        {
            throw new System.NotImplementedException();
        }
    }
}
