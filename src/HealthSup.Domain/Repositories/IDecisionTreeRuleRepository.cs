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
    }
}
