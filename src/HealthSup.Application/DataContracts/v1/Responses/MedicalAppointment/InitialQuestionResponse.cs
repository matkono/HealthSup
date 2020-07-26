using HealthSup.Domain.Entities;
using System.Collections.Generic;

namespace HealthSup.Application.DataContracts.v1.Responses.MedicalAppointment
{
    public class InitialQuestionResponse
    {
        public InitialQuestionResponse
        (
            Question question
        )
        {
            Question = question;
        }

        public Question Question { get; set; }
    }
}
