namespace HealthSup.Domain.Entities
{
    public class MedicalAppointment : BaseEntity
    {
        public MedicalAppointment
        (
            int id,
            bool isDiagnostic,
            Patient patient, 
            Node lastNode
        )
        {
            Id = id;
            IsDiagnostic = isDiagnostic;
            Patient = patient;
            LastNode = lastNode;
        }

        public MedicalAppointment() { }

        public int Id { get; private set; }

        public bool IsDiagnostic { get; private set; }

        public Patient Patient { get; private set; }

        public DecisionTree DecisionTree { get; private set; }

        public Node? LastNode { get; private set; }

        public void setPatient
        (
            Patient patient
        )
        {
            Patient = patient;
        }

        public void setDecisionTree
        (
            DecisionTree decisionTree
        )
        {
            DecisionTree = decisionTree;
        }

        public void setLastNode
        (
            Node node
        )
        {
            LastNode = node;
        }
    }
}
