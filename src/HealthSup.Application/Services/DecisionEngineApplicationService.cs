using HealthSup.Application.DataContracts.Responses;
using HealthSup.Application.DataContracts.v1.Requests.DecisionEngine;
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
            IAnswerQuestionValidator answerQuestionValidator,
            IConfirmActionValidator confirmActionValidator,
            IDecisionEngineDomainService decisionEngineDomainService
        )
        {
            _unitOfWork = unitOfWork;
            AnswerQuestionValidator = answerQuestionValidator;
            ConfirmActionValidator = confirmActionValidator;
            DecisionEngineService = decisionEngineDomainService ?? throw new ArgumentNullException(nameof(decisionEngineDomainService));
        }

        private readonly IUnitOfWork _unitOfWork;
        private readonly IAnswerQuestionValidator AnswerQuestionValidator;
        private readonly IConfirmActionValidator ConfirmActionValidator;
        private readonly IDecisionEngineDomainService DecisionEngineService;

        public async Task<GetNextNodeReturn> AnswerQuestion
        (
            AnswerQuestionRequest argument
        )
        {
            var resultValidator = await AnswerQuestionValidator.ValidateAsync(argument);

            if (!resultValidator.IsValid)
            {
                var response = new GetNextNodeReturn(null);

                foreach (var error in resultValidator.Errors)
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

        public async Task<BaseResponse> ConfirmAction
        (
            ConfirmActionRequest argument
        )
        {
            var response = new BaseResponse();

            var resultValidator = await ConfirmActionValidator.ValidateAsync(argument);

            if (!resultValidator.IsValid)
            {
                foreach (var error in resultValidator.Errors)
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

            await DecisionEngineService.ConfirmAction(argument.MedicalAppointmentId);

            return response;
        }
    }
}
