using HealthSup.Domain.Entities;
using System.Threading.Tasks;

namespace HealthSup.Domain.Repositories
{
    public interface IMedicalAppointmentRepository
    {
        Task<MedicalAppointment> GetById
        (
            int id
        );

        public Task<int> UpdateLastNode
        (
            int id,
            int lastNodeId
        );

        public Task<int> UpdateStatus
        (
            int id,
            int statusId
        );
    }
}
