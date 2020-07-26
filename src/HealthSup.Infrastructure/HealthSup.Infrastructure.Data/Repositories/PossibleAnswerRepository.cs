using Dapper;
using HealthSup.Domain.Entities;
using HealthSup.Domain.Repositories;
using HealthSup.Infrastructure.Data.Scripts;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HealthSup.Infrastructure.Data.Repositories
{
    public class PossibleAnswerRepository : IPossibleAnswerRepository
    {
        private IUnitOfWork UnitOfWork { get; }

        public PossibleAnswerRepository
        (
            IUnitOfWork unitOfWork
        )
        {
            UnitOfWork = unitOfWork;
        }

        public async Task<List<PossibleAnswer>> ListByQuestionId
        (
            int questionId
        )
        {
            var query = ScriptManager.GetByName(ScriptManager.FileNames.PossibleAnswer.ListByQuestionId);

            var result = await UnitOfWork.Connection.QueryAsync<PossibleAnswer>(
                                                                query,
                                                                new { questionId },
                                                                UnitOfWork.Transaction);

            return result.ToList();
        }
    }
}
