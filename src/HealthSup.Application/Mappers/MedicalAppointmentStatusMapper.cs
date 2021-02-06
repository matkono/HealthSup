using HealthSup.Application.DataContracts.v1.Responses.MedicalAppointmentStatus;
using HealthSup.Domain.Entities;

namespace HealthSup.Application.Mappers
{
    public static class MedicalAppointmentStatusMapper
    {
        public static MedicalAppointmentStatusResponse ToDataContract(this MedicalAppointmentStatus medicalAppointmentStatus)
            => new MedicalAppointmentStatusResponse()
            {
                Id = medicalAppointmentStatus.Id,
                Name = medicalAppointmentStatus.Name
            };
    }
}
