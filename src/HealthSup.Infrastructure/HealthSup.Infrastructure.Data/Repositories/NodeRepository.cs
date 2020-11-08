using Dapper;
using HealthSup.Domain.Entities;
using HealthSup.Domain.Repositories;
using HealthSup.Infrastructure.Data.Scripts;
using System.Linq;
using System.Threading.Tasks;

namespace HealthSup.Infrastructure.Data.Repositories
{
    public class NodeRepository : INodeRepository
    {
        public NodeRepository
        (
            IUnitOfWork unitOfWork
        )
        {
            UnitOfWork = unitOfWork;
        }

        private IUnitOfWork UnitOfWork { get; }

        public async Task<Node> GetInitialByDecisionTreeId
        (
            int decisionTreeId
        )
        {
            Node MapFromQuery
            (
                Node node,
                NodeType nodeType,
                DecisionTree decisionTree
            )
            {
                node.SetNodeType(nodeType);

                node.SetDecisionTree(decisionTree);

                return node;
            };

            var query = ScriptManager.GetByName(ScriptManager.FileNames.Node.GetInitialByDecisionTreeid);

            var result = await UnitOfWork.Connection.QueryAsync<Node, NodeType, DecisionTree, Node>(
                                                                query,
                                                                MapFromQuery,
                                                                new { decisionTreeId },
                                                                UnitOfWork.Transaction,
                                                                splitOn: "id, id, id");

            return result.FirstOrDefault();
        }

        public async Task<Node> GetById
        (
            int id
        )
        {
            Node MapFromQuery
            (
                Node node,
                NodeType nodeType,
                DecisionTree decisionTree
            )
            {
                node.SetNodeType(nodeType);

                node.SetDecisionTree(decisionTree);

                return node;
            };

            var query = ScriptManager.GetByName(ScriptManager.FileNames.Node.GetById);

            var result = await UnitOfWork.Connection.QueryAsync<Node, NodeType, DecisionTree, Node>(
                                                                query,
                                                                MapFromQuery,
                                                                new { id },
                                                                UnitOfWork.Transaction,
                                                                splitOn: "id, id, id");

            return result.FirstOrDefault();
        }
    }
}
