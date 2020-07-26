using HealthSup.Domain.Entities;
using System.Threading.Tasks;

namespace HealthSup.Domain.Services.Contracts
{
    public interface IQuestionDomainService
    {
        public Task<Question> GetInitialByDisease
        (
            int diseaseId
        );
    }
}
