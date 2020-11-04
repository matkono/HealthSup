using System;

namespace HealthSup.Domain.Entities
{
    public class Answer
    {
        public Answer
        (
            DateTime date,
            Question question,
            PossibleAnswer possibleAnswer,
            Doctor doctor,
            MedicalAppointment medicalAppointment
        ) 
        {
            Date = date;
            Question = question;
            PossibleAnswer = possibleAnswer;
            Doctor = doctor;
            MedicalAppointment = medicalAppointment;
        }

        public Answer(){ }

        public int Id { get; set; }

        public DateTime Date { get; set; }

        public Question Question { get; set; }

        public PossibleAnswer PossibleAnswer { get; set; }

        public Doctor Doctor { get; set; }

        public MedicalAppointment MedicalAppointment { get; set; }
    }
}
