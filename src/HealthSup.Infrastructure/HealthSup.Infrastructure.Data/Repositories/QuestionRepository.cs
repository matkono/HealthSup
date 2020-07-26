using Dapper;
using HealthSup.Domain.Entities;
using HealthSup.Domain.Repositories;
using HealthSup.Infrastructure.Data.Scripts;
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
            Question MapFromQuery(Question question, QuestionType questionType)
            {
                question.SetQuestionType(questionType);
                return question;
            };

            var query = ScriptManager.GetByName(ScriptManager.FileNames.Question.GetInitialByDiseaseId);

            var result = await UnitOfWork.Connection.QueryAsync<Question, QuestionType, Question>(
                                                                query,
                                                                MapFromQuery,
                                                                new { diseaseId },
                                                                UnitOfWork.Transaction,
                                                                splitOn: "id");

            return result.FirstOrDefault();
        }
    }
}
