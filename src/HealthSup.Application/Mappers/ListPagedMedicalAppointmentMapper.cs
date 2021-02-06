using HealthSup.Application.DataContracts.v1.Responses.MedicalAppointment;
using HealthSup.Domain.Entities;
using System.Collections.Generic;
using System.Linq;

namespace HealthSup.Application.Mappers
{
    public static class ListPagedMedicalAppointmentMapper
    {
        public static ListMedicalAppointmentsPagedByPatientIdResponse ToDataContract(this PagedResult<List<MedicalAppointment>> medicalAppointments)
            => new ListMedicalAppointmentsPagedByPatientIdResponse()
            {
                MedicalAppointments = medicalAppointments.Data.Select(medicalAppointment => medicalAppointment.ToDataContract()).ToList(),
                PageNumber = medicalAppointments.PageNumber,
                PageSize = medicalAppointments.PageSize,
                TotalRows = medicalAppointments.TotalRows
            };
    }
}
