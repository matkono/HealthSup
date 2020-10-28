using Dapper;
using HealthSup.Domain.Entities;
using HealthSup.Domain.Repositories;
using HealthSup.Infrastructure.Data.Scripts;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HealthSup.Infrastructure.Data.Repositories
{
    public class PossibleAnswerRepository: IPossibleAnswerRepository
    {
        public PossibleAnswerRepository
        (
            IUnitOfWork unitOfWork
        )
        {
            UnitOfWork = unitOfWork;
        }

        private IUnitOfWork UnitOfWork { get; }

        public async Task<List<PossibleAnswer>> ListByQuestionId
        (
            int questionId
        )
        {
            PossibleAnswer MapFromQuery
            (
                PossibleAnswer possibleAnswer,
                PossibleAnswerGroup possibleAnswerGroup
            )
            {
                possibleAnswer.SetPossibleAnswerGroup(possibleAnswerGroup);

                return possibleAnswer;
            };

            var query = ScriptManager.GetByName(ScriptManager.FileNames.PossibleAnswer.ListByQuestionId);

            var result = await UnitOfWork.Connection.QueryAsync<PossibleAnswer, PossibleAnswerGroup, PossibleAnswer>(
                                                                query,
                                                                MapFromQuery,
                                                                new { questionId },
                                                                UnitOfWork.Transaction,
                                                                splitOn: "id, id");

            return result.ToList();
        }
    }
}
