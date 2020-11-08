using Dapper;
using HealthSup.Domain.Entities;
using HealthSup.Domain.Repositories;
using HealthSup.Infrastructure.Data.Scripts;
using System.Linq;
using System.Threading.Tasks;

namespace HealthSup.Infrastructure.Data.Repositories
{
    public class PossibleAnswerGroupRepository : IPossibleAnswerGroupRepository
    {
        public PossibleAnswerGroupRepository
        (
            IUnitOfWork unitOfWork
        )
        {
            UnitOfWork = unitOfWork;
        }

        private IUnitOfWork UnitOfWork { get; }

        public async Task<PossibleAnswerGroup> GetById
        (
            int id
        )
        {
            var query = ScriptManager.GetByName(ScriptManager.FileNames.PossibleAnswerGroup.GetById);

            var result = await UnitOfWork.Connection.QueryAsync<PossibleAnswerGroup>(
                                                                query,
                                                                new { id },
                                                                UnitOfWork.Transaction);

            return result.FirstOrDefault();
        }
    }
}
