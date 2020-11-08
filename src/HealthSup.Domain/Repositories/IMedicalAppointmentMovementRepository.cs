using HealthSup.Domain.Entities;
using System.Threading.Tasks;

namespace HealthSup.Domain.Repositories
{
    public interface IMedicalAppointmentMovementRepository
    {
        public Task<int> InsetMovement
        (
            MedicalAppointmentMovement medicalAppointmentMovement
        );

        public Task<MedicalAppointmentMovement> GetByFromNodeId
        (
            int fromNodeId
        );
    }
}
