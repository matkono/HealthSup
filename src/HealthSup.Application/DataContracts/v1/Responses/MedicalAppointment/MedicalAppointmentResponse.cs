using HealthSup.Application.DataContracts.v1.Responses.Node;
using HealthSup.Application.DataContracts.v1.Responses.Patient;
using System.Runtime.Serialization;

namespace HealthSup.Application.DataContracts.v1.Responses.MedicalAppointment
{
    public class MedicalAppointmentResponse
    {
        public int Id { get; private set; }

        public bool IsDiagnostic { get; private set; }

        public PatientResponse Patient { get; private set; }

        public NodeResponse? LastNode { get; private set; }
    }
}
