using System.Threading.Tasks;

namespace Cardiompp.Domain.Services.Contracts
{
    public interface IDoctorDomainService
    {
        Task UpdatePassword(int doctorId, string newPassword);
    }
}
