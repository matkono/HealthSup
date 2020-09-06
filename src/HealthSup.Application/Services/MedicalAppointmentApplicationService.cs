using HealthSup.Application.DataContracts.v1.Requests.MedicalAppointment;
using HealthSup.Application.DataContracts.v1.Responses.Node;
using HealthSup.Application.Services.Contracts;
using HealthSup.Domain.Services.Contracts;
using System;
using System.Threading.Tasks;

namespace HealthSup.Application.Services
{
    public class MedicalAppointmentApplicationService : IMedicalAppointmentApplicationService
    {
        public MedicalAppointmentApplicationService
        (
            IMedicalAppointmentDomainService medicalAppointmentService
        )
        {
            MedicalAppointmentService = medicalAppointmentService ?? throw new ArgumentNullException(nameof(medicalAppointmentService));
        }

        private readonly IMedicalAppointmentDomainService MedicalAppointmentService;

        public Task<NodeResponse> AuthenticateAgentAsync
        (
            GetMedicalAppointmentLastNodeRequest argument
        )
        {
            throw new System.NotImplementedException();
        }
    }
}
