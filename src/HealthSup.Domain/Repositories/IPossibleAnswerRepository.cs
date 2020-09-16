using HealthSup.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HealthSup.Domain.Repositories
{
    public interface IPossibleAnswerRepository
    {
        Task<List<PossibleAnswer>> ListByQuestionId
        (
            int questionId
        );
    }
}
