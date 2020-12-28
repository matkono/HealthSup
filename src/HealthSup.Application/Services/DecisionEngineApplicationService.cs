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
            IDecisionEngineDomainService decisionEngineDomainService,
            IGetPreviousNodeValidator getPreviousNodeValidator,
            IConfirmDecisionValidator confirmDecisionValidator
        )
        {
            _unitOfWork = unitOfWork;
            AnswerQuestionValidator = answerQuestionValidator;
            ConfirmActionValidator = confirmActionValidator;
            GetPreviousNodeValidator = getPreviousNodeValidator;
            ConfirmDecisionValidator = confirmDecisionValidator;
            DecisionEngineService = decisionEngineDomainService ?? throw new ArgumentNullException(nameof(decisionEngineDomainService));
        }

        private readonly IUnitOfWork _unitOfWork;
        private readonly IAnswerQuestionValidator AnswerQuestionValidator;
        private readonly IConfirmActionValidator ConfirmActionValidator;
        private readonly IGetPreviousNodeValidator GetPreviousNodeValidator;
        private readonly IConfirmDecisionValidator ConfirmDecisionValidator;
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

        public async Task<GetNextNodeReturn> ConfirmAction
        (
            ConfirmActionRequest argument
        )
        {
            var resultValidator = await ConfirmActionValidator.ValidateAsync(argument);

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

            var node = await DecisionEngineService.ConfirmAction(argument.MedicalAppointmentId);

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

        public async Task<BaseResponse> ConfirmDecision
        (
            ConfirmDecisionRequest argument    
        ) 
        {
            var response = new BaseResponse();

            var resultValidator = await ConfirmDecisionValidator.ValidateAsync(argument);

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

            await DecisionEngineService.ConfirmDecision(argument.MedicalAppointmentId, argument.DecisionId);

            return response;
        }

        public async Task<GetPreviousNodeReturn> GetPreviousNode
        (
            GetPreviousNodeRequest argument
        )
        {
            var resultValidator = await GetPreviousNodeValidator.ValidateAsync(argument);

            if (!resultValidator.IsValid)
            {
                var response = new GetPreviousNodeReturn(null);

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

            var node = await DecisionEngineService.ResolvePreviousNode
            (
                argument.MedicalAppointmentId
            );

            if (node is Action action)
            {
                return new GetPreviousNodeReturn(action.ToDataContract());
            }
            else if (node is Question question)
            {
                return new GetPreviousNodeReturn(question.ToDataContract());
            }
            else
            {
                var decision = node as Decision;
                return new GetPreviousNodeReturn(decision.ToDataContract());
            }
        }
    }
}
