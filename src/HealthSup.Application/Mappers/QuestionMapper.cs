using HealthSup.Application.DataContracts.v1.Responses.Question;
using HealthSup.Domain.Entities;
using System.Linq;

namespace HealthSup.Application.Mappers
{
    public static class QuestionMapper
    {
        public static QuestionResponse ToDataContract(this Question question)
            => new QuestionResponse()
            {
                Id = question.Id,
                IsInitial = question.IsInitial,
                NodeType = question.NodeType?.ToDataContract(),
                DecisionTree = question.DecisionTree?.ToDataContract(),
                Question = new QuestionBase() 
                {
                    Id = question.QuestionId,
                    Title = question.Title,
                    Code = question.Code,
                    QuestionType = question.QuestionType?.ToDataContract(),
                    PossibleAnswers = question.PossibleAnswers.Select(possibleAnswer => possibleAnswer.ToDataContract()).ToList()
                }
            };
    }
}
