using HealthSup.Application.DataContracts.v1.Responses.MedicalAppointment;
using HealthSup.Domain.Entities;

namespace HealthSup.Application.Mappers
{
    public static class MedicalAppointmentMapper
    {
        public static MedicalAppointmentResponse ToDataContract(this MedicalAppointment medicalAppointment)
            => new MedicalAppointmentResponse()
            {
                Id = medicalAppointment.Id,
                IsDiagnostic = medicalAppointment.IsDiagnostic,
                CurrentNode = medicalAppointment.CurrentNode?.ToDataContract(),
                DecisionTree = medicalAppointment.DecisionTree?.ToDataContract(),
                Patient = medicalAppointment.Patient?.ToDataContract(),
                Status = medicalAppointment.Status?.ToDataContract()
            };
    }
}
