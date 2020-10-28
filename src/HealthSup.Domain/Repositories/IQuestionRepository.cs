using HealthSup.Domain.Entities;
using System.Threading.Tasks;

namespace HealthSup.Domain.Repositories
{
    public interface IQuestionRepository
    {
        Task<Question> GetById
        (
            int id
        );

        Task<Question> GetByNodeId
        (
            int nodeId
        );
    }
}
