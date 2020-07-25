using HealthSup.Domain.Repositories;
using HealthSup.Infrastructure.CrossCutting.Authentication.DTO;
using HealthSup.Infrastructure.Data.Scripts;
using Dapper;
using System.Linq;
using System.Threading.Tasks;

namespace HealthSup.Infrastructure.Data.Repositories
{
    public class HealthSupAgentRepository : IHealthSupAgentRepository
    {
        private IUnitOfWork UnitOfWork { get; }

        public HealthSupAgentRepository(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }

        public async Task<AgentDTO> GetByKeyAndPassword(string name, string password)
        {
            var query = ScriptManager.GetByName(ScriptManager.FileNames.HealthSupAgent.GetByNameAndPassword);

            var result = await UnitOfWork.Connection.QueryAsync<AgentDTO>(
                                                                query,
                                                                new { name, password },
                                                                UnitOfWork.Transaction);

            return result.FirstOrDefault();
        }
    }
}
