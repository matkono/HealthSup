using HealthSup.Domain.Entities;
using System.Threading.Tasks;

namespace HealthSup.Domain.Repositories
{
    public interface IActionRepository
    {
        Task<Action> GetByNodeId
        (
            int nodeId
        );
    }
}
