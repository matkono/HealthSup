using Dapper;
using HealthSup.Domain.Entities;
using HealthSup.Domain.Repositories;
using HealthSup.Infrastructure.Data.Scripts;
using System.Linq;
using System.Threading.Tasks;

namespace HealthSup.Infrastructure.Data.Repositories
{
    public class DecisionTreeRepository : IDecisionTreeRepository
    {
        public DecisionTreeRepository
        (
            IUnitOfWork unitOfWork
        )
        {
            UnitOfWork = unitOfWork;
        }

        private IUnitOfWork UnitOfWork { get; }

        public async Task<DecisionTree> GetCurrentByDiseaseId
        (
            int diseaseId
        )
        {
            DecisionTree MapFromQuery
           (
               DecisionTree decisionTree,
               Disease disease
           )
            {
                decisionTree.SetDisease(disease);

                return decisionTree;
            };

            var query = ScriptManager.GetByName(ScriptManager.FileNames.DecisionTree.GetByDiseaseId);

            var result = await UnitOfWork.Connection.QueryAsync<DecisionTree, Disease, DecisionTree>(
                                                                query,
                                                                MapFromQuery,
                                                                new { diseaseId },
                                                                UnitOfWork.Transaction);

            return result.FirstOrDefault();
        }
    }
}
