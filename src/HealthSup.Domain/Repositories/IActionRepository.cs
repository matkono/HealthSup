using HealthSup.Domain.Entities;
using System.Threading.Tasks;

namespace HealthSup.Domain.Repositories
{
    public interface IActionRepository
    {
        Task<Action> GetById
        (
            int id
        );

        Task<Action> GetByNodeId
        (
            int nodeId
        );
    }
}
