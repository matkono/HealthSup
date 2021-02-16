using HealthSup.Domain.Entities;
using System.Threading.Tasks;

namespace HealthSup.Domain.Repositories
{
    public interface IDecisionTreeRepository
    {
        Task<DecisionTree> GetCurrentByDiseaseId
        (
            int diseaseId
        );
    }
}
