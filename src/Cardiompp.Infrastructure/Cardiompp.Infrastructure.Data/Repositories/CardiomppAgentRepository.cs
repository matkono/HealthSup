using Cardiompp.Domain.Repositories;
using Cardiompp.Infrastructure.CrossCutting.Authentication.DTO;
using Cardiompp.Infrastructure.Data.Scripts;
using Dapper;
using System.Linq;
using System.Threading.Tasks;

namespace Cardiompp.Infrastructure.Data.Repositories
{
    public class CardiomppAgentRepository : ICardiomppAgentRepository
    {
        private IUnitOfWork UnitOfWork { get; }

        public CardiomppAgentRepository(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }

        public async Task<AgentDTO> GetByKeyAndPassword(string name, string password)
        {
            var query = ScriptManager.GetByName(ScriptManager.FileNames.CardiomppAgent.GetByNameAndPassword);

            var result = await UnitOfWork.Connection.QueryAsync<AgentDTO>(
                                                                query,
                                                                new { name, password },
                                                                UnitOfWork.Transaction);

            return result.FirstOrDefault();
        }
    }
}
