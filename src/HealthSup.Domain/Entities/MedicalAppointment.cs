namespace HealthSup.Domain.Entities
{
    public class MedicalAppointment : BaseEntity
    {
        public MedicalAppointment
        (
            int id,
            bool isDiagnostic,
            Patient patient, 
            Node lastNode,
            MedicalAppointmentStatus status
        )
        {
            Id = id;
            IsDiagnostic = isDiagnostic;
            Patient = patient;
            LastNode = lastNode;
            Status = status;
        }

        public MedicalAppointment() { }

        public int Id { get; set; }

        public bool IsDiagnostic { get; set; }

        public Patient Patient { get; set; }

        public DecisionTree DecisionTree { get; set; }

        public Node? LastNode { get; set; }

        public MedicalAppointmentStatus Status { get; set; }
    }
}
