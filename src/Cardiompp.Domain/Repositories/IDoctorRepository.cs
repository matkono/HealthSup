using Cardiompp.Domain.Entities;
using System.Threading.Tasks;

namespace Cardiompp.Domain.Repositories
{
    public interface IDoctorRepository
    { 
        Task<Doctor> GetByCrm(string crm);
    }
}
