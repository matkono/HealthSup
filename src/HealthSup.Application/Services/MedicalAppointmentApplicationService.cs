using HealthSup.Application.DataContracts.v1.Requests.MedicalAppointment;
using HealthSup.Application.DataContracts.v1.Responses.MedicalAppointment;
using HealthSup.Application.Services.Contracts;
using HealthSup.Domain.Enums;
using HealthSup.Domain.Services.Contracts;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace HealthSup.Application.Services
{
    public class MedicalAppointmentApplicationService : IMedicalAppointmentApplicationService
    {
        public MedicalAppointmentApplicationService
        (
            IQuestionDomainService questionDomainService,
            IPossibleAnswerDomainService possibleAnswerDomainService
        )
        {
            QuestionDomainService = questionDomainService ?? throw new ArgumentNullException(nameof(questionDomainService));
            PossibleAnswerDomainService = possibleAnswerDomainService ?? throw new ArgumentNullException(nameof(possibleAnswerDomainService));
        }

        private readonly IQuestionDomainService QuestionDomainService;

        private readonly IPossibleAnswerDomainService PossibleAnswerDomainService;

        public async Task<GetInitialQuestionResponse> GetInitialByDiseaseId
        (
            GetInitialQuestionRequest getInitialQuestionRequest
        )
        {
            var response = new GetInitialQuestionResponse(null);

            var question = await QuestionDomainService.GetInitialByDisease
            (
                getInitialQuestionRequest.DiseaseId
            );

            if (question == null)
            {
                response.AddError
                (
                    (int)ValidationErrorCodeEnum.DiseaseNotFound,
                    $"Disease with Id {getInitialQuestionRequest.DiseaseId} not found.",
                    "DiseaseId"
                );

                return response;
            }

            var possibleAnswers = await PossibleAnswerDomainService.ListByQuestionId
            (
                question.Id
            );

            var initialQuestionResponse = new InitialQuestionResponse(question, possibleAnswers);

            return new GetInitialQuestionResponse(initialQuestionResponse);
        }
    }
}
