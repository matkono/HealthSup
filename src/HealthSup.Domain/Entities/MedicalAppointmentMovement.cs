namespace HealthSup.Domain.Entities
{
    public class MedicalAppointmentMovement: BaseEntity
    {
        public MedicalAppointmentMovement
        (
            Node fromNode,
            Node toNode,
            MedicalAppointment medicalAppointment
        )
        {
            FromNode = fromNode;
            ToNode = toNode;
            MedicalAppointment = medicalAppointment;
        }

        public MedicalAppointmentMovement() { }

        public int Id { get; private set; }

        public Node FromNode { get; private set; }

        public Node ToNode { get; private set; }

        public MedicalAppointment MedicalAppointment { get; private set; }

        public void SetFromNode
        (
            Node fromNode
        )
        {
            FromNode = fromNode;
        }

        public void SetToNode
        (
            Node toNode
        )
        {
            ToNode = toNode;
        }

        public void SetMedicalAppointment
        (
            MedicalAppointment medicalAppointment
        )
        {
            MedicalAppointment = medicalAppointment;
        }
    }
}
