using HealthSup.Application.DataContracts.v1.Responses.Node;
using HealthSup.Application.DataContracts.v1.Responses.Patient;
using System.Runtime.Serialization;

namespace HealthSup.Application.DataContracts.v1.Responses.MedicalAppointment
{
    [DataContract]
    public class MedicalAppointmentResponse
    {
        [DataMember]
        public int Id { get; private set; }

        [DataMember]
        public bool IsDiagnostic { get; private set; }

        [DataMember]
        public PatientResponse Patient { get; private set; }

        [DataMember]
        public NodeResponse? LastNode { get; private set; }
    }
}
