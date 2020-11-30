using HealthSup.Domain.Entities;
using System.Threading.Tasks;

namespace HealthSup.Domain.Repositories
{
    public interface IDecisionTreeRuleRepository
    {
        Task<DecisionTreeRule> GetActionConfirmationQuestionByNodeId
        (
            int nodeId
        );

        Task<DecisionTreeRule> GetByFromNodeIdAndPossibleAnswerIdAsync
        (
            int fromNodeId,
            int possibleAnswerGroupId
        );

        Task<DecisionTreeRule> GetByToNodeIdAsync
        (
            int toNodeId    
        );
    }
}
