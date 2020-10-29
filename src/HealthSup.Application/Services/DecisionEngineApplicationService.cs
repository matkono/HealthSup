using HealthSup.Application.DataContracts.v1.Requests.Node;
using HealthSup.Application.DataContracts.v1.Responses.Node;
using HealthSup.Application.Services.Contracts;
using HealthSup.Application.Validators;
using HealthSup.Domain.Repositories;
using System.Threading.Tasks;

namespace HealthSup.Application.Services
{
    public class DecisionEngineApplicationService : IDecisionEngineApplicationService
    {
        public DecisionEngineApplicationService
        (
            IUnitOfWork unitOfWork
        )
        {
            _unitOfWork = unitOfWork;
        }

        private readonly IUnitOfWork _unitOfWork;

        public async Task<GetNextNodeReturn> GetNextNode
        (
            GetNextNodeRequest argument
        )
        {
            var validator = new GetNextNodeValidator(_unitOfWork);

            var result = validator.Validate(argument);

            throw new System.NotImplementedException();
        }
    }
}
