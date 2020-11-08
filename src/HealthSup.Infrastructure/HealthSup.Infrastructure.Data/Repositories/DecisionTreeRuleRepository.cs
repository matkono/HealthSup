using Dapper;
using HealthSup.Domain.Entities;
using HealthSup.Domain.Repositories;
using HealthSup.Infrastructure.Data.Scripts;
using System.Linq;
using System.Threading.Tasks;

namespace HealthSup.Infrastructure.Data.Repositories
{
    public class DecisionTreeRuleRepository: IDecisionTreeRuleRepository
    {
        public DecisionTreeRuleRepository
        (
            IUnitOfWork unitOfWork
        )
        {
            UnitOfWork = unitOfWork;
        }

        private IUnitOfWork UnitOfWork { get; }

        public async Task<DecisionTreeRule> GetActionConfirmationQuestionByNodeId
        (
            int fromNodeId
        )
        {
            DecisionTreeRule MapFromQuery
            (
                DecisionTreeRule decisionTreeRule,
                Node node,
                Node nextNode,
                PossibleAnswerGroup possibleAnswerGroup
            )
            {
                decisionTreeRule.SetFromNode(node);
                decisionTreeRule.SetToNode(nextNode);
                decisionTreeRule.SetPossibleAnswerGroup(possibleAnswerGroup);

                return decisionTreeRule;
            };

            var query = ScriptManager.GetByName(ScriptManager.FileNames.DecisionTreeRule.GetActionConfirmationQuestionByNodeId);

            var result = await UnitOfWork.Connection.QueryAsync<DecisionTreeRule, Node, Node, PossibleAnswerGroup, DecisionTreeRule>(
                                                                query,
                                                                MapFromQuery,
                                                                new { fromNodeId },
                                                                UnitOfWork.Transaction,
                                                                splitOn: "id, id, id, id");

            return result.FirstOrDefault();
        }
    }
}
