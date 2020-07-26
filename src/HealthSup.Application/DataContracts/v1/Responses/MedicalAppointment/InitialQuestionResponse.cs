using HealthSup.Domain.Entities;
using System.Collections.Generic;

namespace HealthSup.Application.DataContracts.v1.Responses.MedicalAppointment
{
    public class InitialQuestionResponse
    {
        public InitialQuestionResponse
        (
            Question question,
            List<PossibleAnswer> possibleAnswers
        )
        {
            Question = question;
            PossibleAnswers = possibleAnswers;
        }

        public Question Question { get; set; }

        public List<PossibleAnswer> PossibleAnswers { get; set; }
    }
}
