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
            int medicalAppointmentId,
            int fromNodeId
        );

        public Task<MedicalAppointmentMovement> GetByToNodeId
        (
            int medicalAppointmentId,
            int toNodeId
        );

        public Task<MedicalAppointmentMovement> GetLastByMedicalAppointmentId
        (
            int medicalAppointmentId
        );

        public Task<int> DeleteById
        (
            int id
        );
    }
}
