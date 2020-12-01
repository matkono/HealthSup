using HealthSup.Application.DataContracts.v1.Responses.PossibleAnswer;
using HealthSup.Application.DataContracts.v1.Responses.QuestionType;
using System.Collections.Generic;

namespace HealthSup.Application.DataContracts.v1.Responses.Question
{
    public class QuestionBase
    {
        public int Id { get; set; }

        public int Code { get; set; }

        public string Title { get; set; }

        public QuestionTypeResponse QuestionType { get; set; }

        public List<PossibleAnswerResponse> PossibleAnswers { get; set; }
    }
}
