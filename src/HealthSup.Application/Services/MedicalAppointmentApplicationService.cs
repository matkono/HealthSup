using HealthSup.Application.DataContracts.v1.Responses.MedicalAppointment;
using HealthSup.Application.Services.Contracts;
using HealthSup.Domain.Entities;
using HealthSup.Domain.Enums;
using HealthSup.Domain.Services.Contracts;
using System;
using System.Threading.Tasks;

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

            if (medicalAppointment != null)
            {
                Node node;

                if (medicalAppointment.LastNode == null)
                {
                    node = await NodeService.GetInitialByDecisionTreeId(medicalAppointment.DecisionTree.Id);
                    await MedicalAppointmentService.UpdatelastNode(medicalAppointment.Id, node.Id);
                }
                else
                {
                    node = await NodeService.GetById(medicalAppointment.LastNode.Id);
                }

                return new GetMedicalAppointmentLastNodeReturn(null);
            }
            else 
            {
                var response = new GetMedicalAppointmentLastNodeReturn(null);

                response.AddError
                (
                    (int)ValidationErrorCodeEnum.MedicalAppointNotFOund,
                    "Medical appointment not found.",
                    null
                );

                return response;
            }
        }
    }
}
