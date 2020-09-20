using HealthSup.Domain.Entities;
using System.Threading.Tasks;

namespace HealthSup.Domain.Services.Contracts
{
    public interface INodeDomainService
    {
        Task<Node> GetInitialByDecisionTreeId
        (
            int decisionTreeId
        );

        Task<Node> ResolveByMedicalAppointment
        (
            MedicalAppointment medicalAppointment
        );
    }
}
