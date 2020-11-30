using HealthSup.Domain.Entities;
using System.Threading.Tasks;

namespace HealthSup.Domain.Repositories
{
    public interface IDecisionRepository
    {
        Task<Decision> GetById
        (
            int id
        );

        Task<Decision> GetByNodeId
        (
            int nodeId
        );
    }
}
