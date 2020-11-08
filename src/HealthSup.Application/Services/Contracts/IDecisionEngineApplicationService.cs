using HealthSup.Application.DataContracts.v1.Requests.Node;
using HealthSup.Application.DataContracts.v1.Responses.Node;
using System.Threading.Tasks;

namespace HealthSup.Application.Services.Contracts
{
    public interface IDecisionEngineApplicationService
    {
        public Task<GetNextNodeReturn> AnswerQuestion
        (
            AnswerQuestionRequest argument
        );
    }
}
