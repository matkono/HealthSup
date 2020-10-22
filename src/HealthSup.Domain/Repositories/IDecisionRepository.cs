using HealthSup.Domain.Entities;
using System.Threading.Tasks;

namespace HealthSup.Domain.Repositories
{
    public interface IDecisionRepository
    {
        Task<Decision> GetByNodeId
        (
            int nodeId
        );
    }
}
