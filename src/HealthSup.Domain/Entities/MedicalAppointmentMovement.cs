namespace HealthSup.Domain.Entities
{
    public class MedicalAppointmentMovement: BaseEntity
    {
        public MedicalAppointmentMovement
        (
            int id,
            Node fromNode,
            Node toNode,
            MedicalAppointment medicalAppointment
        )
        {
            Id = id;
            FromNode = fromNode;
            ToNode = toNode;
            MedicalAppointment = medicalAppointment;
        }

        public MedicalAppointmentMovement() { }

        public int Id { get; set; }

        public Node FromNode { get; set; }

        public Node ToNode { get; set; }

        public MedicalAppointment MedicalAppointment { get; set; }
    }
}
