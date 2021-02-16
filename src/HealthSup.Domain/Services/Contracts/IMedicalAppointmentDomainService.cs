using HealthSup.Domain.Entities;
using System.Threading.Tasks;

namespace HealthSup.Domain.Services.Contracts
{
    public interface IMedicalAppointmentDomainService
    {
        Task<MedicalAppointment> GetById
        (
            int id
        );

        Task<MedicalAppointment> Create
        (
            int patientId, 
            int diseaseId
        );
    }
}
