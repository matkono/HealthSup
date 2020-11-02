using HealthSup.Application.DataContracts.v1.Requests.Node;
using HealthSup.Application.DataContracts.v1.Responses.Node;
using HealthSup.Application.Services.Contracts;
using HealthSup.Application.Validators.Contracts;
using HealthSup.Domain.Repositories;
using System;
using System.Threading.Tasks;

namespace HealthSup.Application.Services
{
    public class DecisionEngineApplicationService : IDecisionEngineApplicationService
    {
        public DecisionEngineApplicationService
        (
            IUnitOfWork unitOfWork,
            IGetNextNodeValidator getNextNodeValidator
        )
        {
            _unitOfWork = unitOfWork;
            _getNextNodeValidator = getNextNodeValidator;
        }

        private readonly IUnitOfWork _unitOfWork;
        private readonly IGetNextNodeValidator _getNextNodeValidator;

        public async Task<GetNextNodeReturn> GetNextNode
        (
            GetNextNodeRequest argument
        )
        {
            var result = await  _getNextNodeValidator.ValidateAsync(argument);

            if (!result.IsValid)
            {
                var response = new GetNextNodeReturn(null);

                foreach (var error in result.Errors)
                {
                    response.AddError
                    (
                        Int32.Parse(error.ErrorCode),
                        error.ErrorMessage,
                        error.PropertyName
                    );
                }

                return response;
            }

            return new GetNextNodeReturn(null);
        }
    }
}
