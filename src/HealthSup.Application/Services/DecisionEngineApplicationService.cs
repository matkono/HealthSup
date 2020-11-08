using HealthSup.Application.DataContracts.v1.Requests.Node;
using HealthSup.Application.DataContracts.v1.Responses.Node;
using HealthSup.Application.Mappers;
using HealthSup.Application.Services.Contracts;
using HealthSup.Application.Validators.Contracts;
using HealthSup.Domain.Entities;
using HealthSup.Domain.Repositories;
using HealthSup.Domain.Services.Contracts;
using System;
using System.Threading.Tasks;
using Action = HealthSup.Domain.Entities.Action;

namespace HealthSup.Application.Services
{
    public class DecisionEngineApplicationService : IDecisionEngineApplicationService
    {
        public DecisionEngineApplicationService
        (
            IUnitOfWork unitOfWork,
            IAnswerQuestionValidator getNextNodeValidator,
            IDecisionEngineDomainService decisionEngineDomainService
        )
        {
            _unitOfWork = unitOfWork;
            _getNextNodeValidator = getNextNodeValidator;
            DecisionEngineService = decisionEngineDomainService ?? throw new ArgumentNullException(nameof(decisionEngineDomainService));
        }

        private readonly IUnitOfWork _unitOfWork;
        private readonly IAnswerQuestionValidator _getNextNodeValidator;
        private readonly IDecisionEngineDomainService DecisionEngineService;

        public async Task<GetNextNodeReturn> AnswerQuestion
        (
            AnswerQuestionRequest argument
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

            var node = await DecisionEngineService.ResolveNextNode
            (
                argument.MedicalAppointmentId,
                argument.DoctorId,
                argument.QuestionId,
                argument.PossibleAnswerGroupId,
                argument.Date,
                argument.PossibleAnswersId
            );

            if (node is Action action)
            {
                return new GetNextNodeReturn(action.ToDataContract());
            }
            else if (node is Question question)
            {
                return new GetNextNodeReturn(question.ToDataContract());
            }
            else
            {
                var decision = node as Decision;
                return new GetNextNodeReturn(decision.ToDataContract());
            }
        }
    }
}
