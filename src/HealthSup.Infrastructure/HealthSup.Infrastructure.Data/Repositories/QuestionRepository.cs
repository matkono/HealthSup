using Dapper;
using HealthSup.Domain.Entities;
using HealthSup.Domain.Repositories;
using HealthSup.Infrastructure.Data.Scripts;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HealthSup.Infrastructure.Data.Repositories
{
    public class QuestionRepository : IQuestionRepository
    {
        private IUnitOfWork UnitOfWork { get; }

        public QuestionRepository
        (
            IUnitOfWork unitOfWork
        )
        {
            UnitOfWork = unitOfWork;
        }

        public async Task<Question> GetInitialByDisease
        (
            int diseaseId
        )
        {
            var query = ScriptManager.GetByName(ScriptManager.FileNames.Question.GetInitialByDiseaseId);

            var questionDictionary = new Dictionary<int, Question>();

            Question MapFromQuery
            (
                Question question,
                QuestionType questionType,
                PossibleAnswer possibleAnswer
            )
            {
                Question questionEntity = null;

                if (!questionDictionary.TryGetValue(question.Id, out questionEntity))
                {
                    questionEntity = question;
                    questionDictionary.Add(questionEntity.Id, questionEntity);
                }

                questionEntity.PossibleAnswers.Add(possibleAnswer);

                questionEntity.SetQuestionType(questionType);

                return questionEntity;
            };

            var result = await UnitOfWork.Connection.QueryAsync<Question, QuestionType, PossibleAnswer, Question>(
                                                                query,
                                                                MapFromQuery,
                                                                new { diseaseId },
                                                                UnitOfWork.Transaction,
                                                                splitOn: "id, id");

            return result.Distinct().ToList().FirstOrDefault();
        }
    }
}
