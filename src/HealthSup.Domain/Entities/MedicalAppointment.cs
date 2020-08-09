using System;

namespace HealthSup.Domain.Entities
{
    public class MedicalAppointment : DomainResponse
    {
        public MedicalAppointment
        (
            int id,
            bool isDiagnostic,
            Patient patient, 
            Doctor doctor,
            Disease disease,
            Node lastNode
        )
        {
            Id = id;
            IsDiagnostic = isDiagnostic;
            Patient = patient;
            Doctor = doctor;
            Disease = disease;
            LastNode = lastNode;
        }

        public MedicalAppointment() { }

        public int Id { get; private set; }

        public bool IsDiagnostic { get; private set; }

        public Patient Patient { get; private set; }

        public Doctor Doctor { get; private set; }

        public Disease Disease { get; private set; }

        public Node? LastNode { get; private set; }
    }
}
