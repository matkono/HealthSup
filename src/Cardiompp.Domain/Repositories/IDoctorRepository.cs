using System.Threading.Tasks;

namespace Cardiompp.Domain.Repositories
{
    public interface IDoctorRepository
    { 
        Task UpdatePassword(int doctorId, string newPassword);
    }
}
