using HealthSup.Domain.Entities;
using System.Threading.Tasks;

namespace HealthSup.Domain.Repositories
{
    public interface IPossibleAnswerGroupRepository
    {
        Task<PossibleAnswerGroup> GetById
        (
            int id
        );
    }
}
