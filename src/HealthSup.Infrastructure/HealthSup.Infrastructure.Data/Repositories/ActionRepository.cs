using Dapper;
using HealthSup.Domain.Entities;
using HealthSup.Domain.Repositories;
using HealthSup.Infrastructure.Data.Scripts;
using System.Linq;
using System.Threading.Tasks;

namespace HealthSup.Infrastructure.Data.Repositories
{
    public class ActionRepository : IActionRepository
    {
        public ActionRepository
        (
            IUnitOfWork unitOfWork
        )
        {
            UnitOfWork = unitOfWork;
        }

        private IUnitOfWork UnitOfWork { get; }

        public async Task<Action> GetById
        (
            int id
        )
        {
            Action MapFromQuery
            (
                Action action,
                NodeType nodeType,
                DecisionTree decisionTree
            )
            {
                action.SetNodeType(nodeType);
                action.SetDecisionTree(decisionTree);

                return action;
            };

            var query = ScriptManager.GetByName(ScriptManager.FileNames.Action.GetById);

            var result = await UnitOfWork.Connection.QueryAsync<Action, NodeType, DecisionTree, Action>(
                                                                query,
                                                                MapFromQuery,
                                                                new { id },
                                                                UnitOfWork.Transaction);

            return result.FirstOrDefault();
        }

        public async Task<Action> GetByNodeId
        (
            int nodeId
        )
        {
            Action MapFromQuery
            (
                Action action,
                NodeType nodeType,
                DecisionTree decisionTree
            )
            {
                action.SetNodeType(nodeType);
                action.SetDecisionTree(decisionTree);

                return action;
            };

            var query = ScriptManager.GetByName(ScriptManager.FileNames.Action.GetByNodeId);

            var result = await UnitOfWork.Connection.QueryAsync<Action, NodeType, DecisionTree, Action>(
                                                                query,
                                                                MapFromQuery,
                                                                new { nodeId },
                                                                UnitOfWork.Transaction,
                                                                splitOn: "actionId, id, id, id");

            return result.FirstOrDefault();
        }
    }
}
