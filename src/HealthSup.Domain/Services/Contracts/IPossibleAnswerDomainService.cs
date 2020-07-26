using HealthSup.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HealthSup.Domain.Services.Contracts
{
    public interface IPossibleAnswerDomainService
    {
        public Task<List<PossibleAnswer>> ListByQuestionId
        (
            int questionId
        );
    }
}
