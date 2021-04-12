using HealthSup.Domain.Entities;
using System.Threading.Tasks;

namespace HealthSup.Domain.Repositories
{
    public interface IDoctorRepository
    {
        Task<Doctor> GetById
        (
            int id
        );

        Task<Doctor> GetByUserId
        (
            int userId
        );
    }
}
