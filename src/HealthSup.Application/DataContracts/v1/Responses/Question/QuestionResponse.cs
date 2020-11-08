using HealthSup.Application.DataContracts.v1.Responses.Node;
using HealthSup.Application.DataContracts.v1.Responses.PossibleAnswer;
using HealthSup.Application.DataContracts.v1.Responses.QuestionType;
using System.Collections.Generic;

namespace HealthSup.Application.DataContracts.v1.Responses.Question
{
    public class QuestionResponse: NodeResponse
    {
        public int QuestionId { get; set; }

        public int Code { get; set; }

        public string Title { get; set; }

        public QuestionTypeResponse QuestionType { get; set; }

        public List<PossibleAnswerResponse> PossibleAnswers { get; set; }
    }
}
