using HealthSup.Application.DataContracts.v1.Requests.MedicalAppointment;
using HealthSup.Application.DataContracts.v1.Responses.Node;
using System.Threading.Tasks;

namespace HealthSup.Application.Services.Contracts
{
    public interface IMedicalAppointmentApplicationService
    {
        public Task<NodeResponse> AuthenticateAgentAsync
        (
            GetMedicalAppointmentLastNodeRequest argument
        );
    }
}
