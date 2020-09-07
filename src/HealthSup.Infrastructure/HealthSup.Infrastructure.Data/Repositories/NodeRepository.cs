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
            var query = ScriptManager.GetByName(ScriptManager.FileNames.Node.GetInitialByDecisionTreeid);

            var result = await UnitOfWork.Connection.QueryAsync<Node>(
                                                                query,
                                                                new { decisionTreeId},
                                                                UnitOfWork.Transaction);

            return result.FirstOrDefault();
        }
    }
}
