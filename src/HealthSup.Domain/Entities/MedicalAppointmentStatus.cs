namespace HealthSup.Domain.Entities
{
    public class MedicalAppointmentStatus
    {
        public MedicalAppointmentStatus
        (
            int id,
            string name
        )
        {
            Id = id;
            Name = name;
        }

        public MedicalAppointmentStatus() { }

        public int Id { get; set; }

        public string Name { get; set; }
    }
}
