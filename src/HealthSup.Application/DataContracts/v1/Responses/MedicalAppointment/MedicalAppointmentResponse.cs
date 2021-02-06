using HealthSup.Application.DataContracts.v1.Responses.DecisionTree;
using HealthSup.Application.DataContracts.v1.Responses.MedicalAppointmentStatus;
using HealthSup.Application.DataContracts.v1.Responses.Node;
using HealthSup.Application.DataContracts.v1.Responses.Patient;

namespace HealthSup.Application.DataContracts.v1.Responses.MedicalAppointment
{
    public class MedicalAppointmentResponse
    {
        public int Id { get; set; }

        public bool IsDiagnostic { get; set; }

        public PatientResponse Patient { get; set; }

        public DecisionTreeResponse DecisionTree { get; set; }

        public NodeResponse CurrentNode { get; set; }

        public MedicalAppointmentStatusResponse Status { get; set; }

    }
}
