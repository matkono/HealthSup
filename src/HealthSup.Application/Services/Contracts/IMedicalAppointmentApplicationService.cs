using HealthSup.Application.DataContracts.v1.Requests.MedicalAppointment;
using HealthSup.Application.DataContracts.v1.Responses.MedicalAppointment;
using System.Threading.Tasks;

namespace HealthSup.Application.Services.Contracts
{
    public interface IMedicalAppointmentApplicationService
    {
        public Task<GetMedicalAppointmentLastNodeReturn> GetLastNode
        (
            int medicalAppointmentId
        );

        public Task<ListMedicalAppointmentsPagedByPatientIdReturn> ListPaged
        (
            ListPagedByPatientIdRequest argument
        );
    }
}
