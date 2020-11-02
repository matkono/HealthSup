using HealthSup.Application.DataContracts.v1.Requests.Node;
using HealthSup.Application.DataContracts.v1.Responses.Node;
using HealthSup.Application.Services.Contracts;
using HealthSup.Application.Validators.Contracts;
using HealthSup.Domain.Repositories;
using HealthSup.Domain.Services.Contracts;
using System;
using System.Threading.Tasks;

namespace HealthSup.Application.Services
{
    public class DecisionEngineApplicationService : IDecisionEngineApplicationService
    {
        public DecisionEngineApplicationService
        (
            IUnitOfWork unitOfWork,
            IGetNextNodeValidator getNextNodeValidator,
            IDecisionEngineDomainService decisionEngineDomainService
        )
        {
            _unitOfWork = unitOfWork;
            _getNextNodeValidator = getNextNodeValidator;
            DecisionEngineService = decisionEngineDomainService ?? throw new ArgumentNullException(nameof(decisionEngineDomainService));
        }

        private readonly IUnitOfWork _unitOfWork;
        private readonly IGetNextNodeValidator _getNextNodeValidator;
        private readonly IDecisionEngineDomainService DecisionEngineService;

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

            var teste = DecisionEngineService.ResolveNextNode
            (
                argument.MedicalAppointmentId,
                argument.DoctorId,
                argument.QuestionId,
                argument.PossibleAnswerGroupId,
                argument.Date,
                argument.PossibleAnswersId
            );

            return new GetNextNodeReturn(null);
        }
    }
}
