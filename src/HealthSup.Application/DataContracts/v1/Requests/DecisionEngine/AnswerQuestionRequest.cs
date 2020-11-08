using System;
using System.Collections.Generic;

namespace HealthSup.Application.DataContracts.v1.Requests.Node
{
    public class AnswerQuestionRequest
    {
        public int MedicalAppointmentId { get; set; }

        public int DoctorId { get; set; }

        public int QuestionId { get; set; }

        public int PossibleAnswerGroupId { get; set; }

        public DateTime Date { get; set; }

        public List<int> PossibleAnswersId { get; set; }
    }
}
