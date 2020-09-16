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

        Task UpdatelastNode
        (
            int id,
            int lastNodeId
        );
    }
}
