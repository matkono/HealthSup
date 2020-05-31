using Cardiompp.Domain.Entities;
using System.Threading.Tasks;

namespace Cardiompp.Domain.Services.Contracts
{
    public interface IDoctorDomainService
    {
        Task<Doctor> GetByCrm(string crm);

        Task<Doctor> GetByEmailAndPassword(string email, string password);

        Task UpdatePassword(int doctorId, string newPassword);
    }
}
