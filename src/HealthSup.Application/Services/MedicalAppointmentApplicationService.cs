using HealthSup.Application.DataContracts.v1.Responses.MedicalAppointment;
using HealthSup.Application.Mappers;
using HealthSup.Application.Services.Contracts;
using HealthSup.Domain.Entities;
using HealthSup.Domain.Enums;
using HealthSup.Domain.Services.Contracts;
using System;
using System.Threading.Tasks;
using Action = HealthSup.Domain.Entities.Action;

namespace HealthSup.Application.Services
{
    public class MedicalAppointmentApplicationService : IMedicalAppointmentApplicationService
    {
        public MedicalAppointmentApplicationService
        (
            IMedicalAppointmentDomainService medicalAppointmentService,
            INodeDomainService nodeService
        )
        {
            MedicalAppointmentService = medicalAppointmentService ?? throw new ArgumentNullException(nameof(medicalAppointmentService));
            NodeService = nodeService ?? throw new ArgumentNullException(nameof(nodeService));
        }

        private readonly IMedicalAppointmentDomainService MedicalAppointmentService;

        private readonly INodeDomainService NodeService;

        public async Task<GetMedicalAppointmentLastNodeReturn> GetLastNode
        (
            int medicalAppointmentId
        )
        {
            var medicalAppointment = await MedicalAppointmentService.GetById(medicalAppointmentId);

            if (medicalAppointment == null)
            {
                var response = new GetMedicalAppointmentLastNodeReturn(null);

                response.AddError
                (
                    (int)ValidationErrorCodeEnum.MedicalAppointNotFound,
                    "Medical appointment not found.",
                    null
                );

                return response;
            }

            var node = await NodeService.GetCurrentNode(medicalAppointment);

            if (node is Action action)
            {
                return new GetMedicalAppointmentLastNodeReturn(action.ToDataContract());
            }
            else if (node is Question question)
            {
                return new GetMedicalAppointmentLastNodeReturn(question.ToDataContract());
            }
            else
            {
                var decision = node as Decision;
                return new GetMedicalAppointmentLastNodeReturn(decision.ToDataContract());
            }
        }
    }
}
