using HealthSup.Application.DataContracts.v1.Requests.MedicalAppointment;
using HealthSup.Application.DataContracts.v1.Responses.MedicalAppointment;
using HealthSup.Application.Services.Contracts;
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
            IMedicalAppointmentDomainService medicalAppointmentService
        )
        {
            MedicalAppointmentService = medicalAppointmentService ?? throw new ArgumentNullException(nameof(medicalAppointmentService));
        }

        private readonly IMedicalAppointmentDomainService MedicalAppointmentService;

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
                    (int)ValidationErrorCodeEnum.MedicalAppointNotFOund,
                    "Medical appointment not found.",
                    null
                );

                return response;
            }

            return new GetMedicalAppointmentLastNodeReturn(null);
        }
    }
}
