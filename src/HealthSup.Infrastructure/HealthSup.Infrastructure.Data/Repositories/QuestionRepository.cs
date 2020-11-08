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
        public QuestionRepository
        (
            IUnitOfWork unitOfWork
        )
        {
            UnitOfWork = unitOfWork;
        }

        private IUnitOfWork UnitOfWork { get; }

        public async Task<Question> GetById
        (
            int id
        )
        {
            Question MapFromQuery
            (
                Question question,
                QuestionType questionType,
                NodeType nodeType,
                DecisionTree decisionTree
            )
            {
                question.SetQuestionType(questionType);
                question.SetNodeType(nodeType);
                question.SetDecisionTree(decisionTree);

                return question;
            };

            var query = ScriptManager.GetByName(ScriptManager.FileNames.Question.GetById);

            var result = await UnitOfWork.Connection.QueryAsync<Question, QuestionType, NodeType, DecisionTree, Question>(
                                                                query,
                                                                MapFromQuery,
                                                                new { id },
                                                                UnitOfWork.Transaction);

            return result.FirstOrDefault();
        }

        public async Task<Question> GetByNodeId
        (
            int nodeId
        )
        {
            Question MapFromQuery
            (
                Question question,
                QuestionType questionType,
                NodeType nodeType,
                DecisionTree decisionTree
            )
            {
                question.SetQuestionType(questionType);
                question.SetNodeType(nodeType);
                question.SetDecisionTree(decisionTree);

                return question;
            };

            var query = ScriptManager.GetByName(ScriptManager.FileNames.Question.GetByNodeId);

            var result = await UnitOfWork.Connection.QueryAsync<Question, QuestionType, NodeType, DecisionTree, Question>(
                                                                query,
                                                                MapFromQuery,
                                                                new { nodeId },
                                                                UnitOfWork.Transaction,
                                                                splitOn: "questionId, id, id, id");

            return result.FirstOrDefault();
        }
    }
}
