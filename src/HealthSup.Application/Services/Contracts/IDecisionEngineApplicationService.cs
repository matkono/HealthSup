using HealthSup.Application.DataContracts.Responses;
using HealthSup.Application.DataContracts.v1.Requests.DecisionEngine;
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

        public Task<GetNextNodeReturn> ConfirmAction
        (
            ConfirmActionRequest argument
        );

        public Task<BaseResponse> ConfirmDecision
        (
            ConfirmDecisionRequest argument
        );

        public Task<GetPreviousNodeReturn> GetPreviousNode
        (
            GetPreviousNodeRequest argument
        );
    }
}
