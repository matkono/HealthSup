using HealthSup.Application.DataContracts.Responses;

namespace HealthSup.Application.DataContracts.v1.Responses.MedicalAppointment
{
    public class ListMedicalAppointmentsPagedByPatientIdReturn : BaseResponse<ListMedicalAppointmentsPagedByPatientIdResponse>
    {
        public ListMedicalAppointmentsPagedByPatientIdReturn(ListMedicalAppointmentsPagedByPatientIdResponse data) : base(data)
        {
        }
    }
}
