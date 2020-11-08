using HealthSup.Domain.Entities;
using System.Threading.Tasks;

namespace HealthSup.Domain.Repositories
{
    public interface IMedicalAppointmentFlowRepository
    {
        public Task<int> InsetMovement
        (
            MedicalAppointmentFlow medicalAppointmentFlow
        );
    }
}
