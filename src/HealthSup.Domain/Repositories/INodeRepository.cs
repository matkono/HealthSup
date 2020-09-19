using HealthSup.Domain.Entities;
using System.Threading.Tasks;

namespace HealthSup.Domain.Repositories
{
    public interface INodeRepository
    {
        Task<Node> GetInitialByDecisionTreeId
        (
            int decisionTreeId
        );

        Task<Node> GetById
        (
            int id
        );
    }
}
