using HealthSup.Application.DataContracts.v1.Responses.Node;
using HealthSup.Application.DataContracts.v1.Responses.PossibleAnswer;
using HealthSup.Application.DataContracts.v1.Responses.QuestionType;
using System.Collections.Generic;

namespace HealthSup.Application.DataContracts.v1.Responses.Question
{
    public class QuestionResponse: NodeResponse
    {
        public QuestionBase Question { get; set; }
    }
}
