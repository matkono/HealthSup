using HealthSup.Domain.Entities;
using HealthSup.Domain.Repositories;
using System.Threading.Tasks;

namespace HealthSup.Infrastructure.Data.Repositories
{
    public class MedicalAppointmentRepository : IMedicalAppointmentRepository
    {
        public Task<MedicalAppointment> GetById(int id)
        {
            throw new System.NotImplementedException();
        }
    }
}
