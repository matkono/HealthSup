using Dapper;
using HealthSup.Domain.Entities;
using HealthSup.Domain.Repositories;
using HealthSup.Infrastructure.Data.Scripts;
using System.Linq;
using System.Threading.Tasks;

namespace HealthSup.Infrastructure.Data.Repositories
{
    public class DecisionRepository : IDecisionRepository
    {
        public DecisionRepository
        (
            IUnitOfWork unitOfWork
        )
        {
            UnitOfWork = unitOfWork;
        }

        private IUnitOfWork UnitOfWork { get; }

        public async Task<Decision> GetById
        (
            int id
        )
        {
            Decision MapFromQuery
            (
                Decision decision,
                NodeType nodeType,
                DecisionTree decisionTree
            )
            {
                decision.SetNodeType(nodeType);
                decision.SetDecisionTree(decisionTree);

                return decision;
            };

            var query = ScriptManager.GetByName(ScriptManager.FileNames.Decision.GetById);

            var result = await UnitOfWork.Connection.QueryAsync<Decision, NodeType, DecisionTree, Decision>(
                                                                query,
                                                                MapFromQuery,
                                                                new { id },
                                                                UnitOfWork.Transaction);

            return result.FirstOrDefault();
        }

        public async Task<Decision> GetByNodeId
        (
            int nodeId
        )
        {
            Decision MapFromQuery
            (
                Decision decision,
                NodeType nodeType,
                DecisionTree decisionTree
            )
            {
                decision.SetNodeType(nodeType);
                decision.SetDecisionTree(decisionTree);

                return decision;
            };

            var query = ScriptManager.GetByName(ScriptManager.FileNames.Decision.GetByNodeId);

            var result = await UnitOfWork.Connection.QueryAsync<Decision, NodeType, DecisionTree, Decision>(
                                                                query,
                                                                MapFromQuery,
                                                                new { nodeId },
                                                                UnitOfWork.Transaction,
                                                                splitOn: "decisionId, id, id, id");

            return result.FirstOrDefault();
        }
    }
}
