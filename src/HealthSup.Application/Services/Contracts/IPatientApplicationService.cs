using HealthSup.Application.DataContracts.v1.Requests;
using HealthSup.Application.DataContracts.v1.Responses.MedicalAppointment;
using HealthSup.Application.DataContracts.v1.Responses.Patient;
using System.Threading.Tasks;

namespace HealthSup.Application.Services.Contracts
{
    public interface IPatientApplicationService
    {
        public Task<ListPatientsPagedReturn> ListPaged
        (
            Pagination pagination
        );

        public Task<GetPatientByRegistrationReturn> GetByRegistration
        (
            string registration
        );

        public Task<ListMedicalAppointmentsPagedByPatientIdReturn> ListMedicalAppointments
        (
            int patientId, 
            Pagination pagination
        );
    }
}
