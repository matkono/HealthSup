using HealthSup.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HealthSup.Domain.Repositories
{
    public interface IPossibleAnswerRepository
    {
        Task<PossibleAnswer> GetById
        (
            int id
        );

        Task<List<PossibleAnswer>> ListByQuestionId
        (
            int questionId
        );
    }
}
