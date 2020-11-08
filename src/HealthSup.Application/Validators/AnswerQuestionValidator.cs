using FluentValidation;
using HealthSup.Application.DataContracts.v1.Requests.Node;
using HealthSup.Application.Validators.Contracts;
using HealthSup.Domain.Enums;
using HealthSup.Domain.Repositories;
using System.Threading;
using System.Threading.Tasks;

namespace HealthSup.Application.Validators
{
    public class AnswerQuestionValidator: AbstractValidator<AnswerQuestionRequest>, IAnswerQuestionValidator
    {
        private readonly IUnitOfWork _unitOfWork;

        public AnswerQuestionValidator
        (
            IUnitOfWork unitOfWork
        )
        {
            _unitOfWork = unitOfWork;

            #region MedicalAppointmentId

            RuleFor(x => x.MedicalAppointmentId)
                .NotEqual(0)
                .WithErrorCode(((int)ValidationErrorCodeEnum.MedicalAppointmentIdIsNull).ToString())
                .WithMessage("MedicalAppointmentId is required, and cannot be 0, to get next node.");

            When(x => !x.MedicalAppointmentId.Equals(0), () => 
            {
                RuleFor(x => x.MedicalAppointmentId)
                .MustAsync(BeValidMedicalAppointmentIdAsync)
                .WithErrorCode(((int)ValidationErrorCodeEnum.MedicalAppointNotFound).ToString())
                .WithMessage("Medical appointment with id {PropertyValue} is not found.");
            });

            WhenAsync((x, cancellationToken) => BeUnfinalizedMedicalAppointment(x.MedicalAppointmentId, cancellationToken), () =>
            {
                RuleFor(x => x.MedicalAppointmentId)
                .MustAsync(BeMedicalAppointmentUnfinalizedAsync)
                .WithErrorCode(((int)ValidationErrorCodeEnum.MedicalAppointmentIsFinalized).ToString())
                .WithMessage("Medical appointment with id {PropertyValue} is finalized.");
            });

            #endregion

            #region DoctorId

            RuleFor(x => x.DoctorId)
                .NotEqual(0)
                .WithErrorCode(((int)ValidationErrorCodeEnum.DoctorIdIsNull).ToString())
                .WithMessage("DoctorId is required, and cannot be 0, to get next node.");

            When(x => !x.DoctorId.Equals(0), () =>
            {
                RuleFor(x => x.DoctorId)
                .MustAsync(BeValidDoctorIdAsync)
                .WithErrorCode(((int)ValidationErrorCodeEnum.DoctorNotFound).ToString())
                .WithMessage("Doctor with id {PropertyValue} is not found.");
            });

            #endregion

            #region QuestionId

            RuleFor(x => x.QuestionId)
                .NotEqual(0)
                .WithErrorCode(((int)ValidationErrorCodeEnum.QuestionIdIsNull).ToString())
                .WithMessage("QuestionId is required, and cannot be 0, to get next node.");

            When(x => !x.QuestionId.Equals(0), () =>
            {
                RuleFor(x => x.QuestionId)
                .MustAsync(BeValidQuestionIdAsync)
                .WithErrorCode(((int)ValidationErrorCodeEnum.QuestionNotFound).ToString())
                .WithMessage("Question with id {PropertyValue} is not found.");
            });

            WhenAsync((x, cancellationToken) => BeCurrenteMedicalAppoinmentNode(x.QuestionId, x.MedicalAppointmentId, cancellationToken), () =>
            {
                RuleFor(x => x.QuestionId)
                .MustAsync(async (x, questionId, cancellationToken) => await BeCurrentNode(x.MedicalAppointmentId, questionId))
                .WithErrorCode(((int)ValidationErrorCodeEnum.QuestionIsNotCurrentNode).ToString())
                .WithMessage("Question with id {PropertyValue} is not the current node of Medical Appoint, so cannot be answered.");
            });

            #endregion

            #region PossibleAnswerGroupId

            RuleFor(x => x.PossibleAnswerGroupId)
                .NotEqual(0)
                .WithErrorCode(((int)ValidationErrorCodeEnum.PossibleAnswerGroupIdIsNull).ToString())
                .WithMessage("PossibleAnswerGroupId is required, and cannot be 0, to get next node.");

            When(x => !x.PossibleAnswerGroupId.Equals(0), () =>
            {
                RuleFor(x => x.PossibleAnswerGroupId)
                .MustAsync(BeValidPossibleAnswerGroupIdAsync)
                .WithErrorCode(((int)ValidationErrorCodeEnum.PossibleAnswerGroupNotFound).ToString())
                .WithMessage("Possible answer group with id {PropertyValue} is not found.");
            });

            #endregion

            #region PossibleAnswersId

            RuleFor(x => x.PossibleAnswersId)
                .NotNull()
                .WithErrorCode(((int)ValidationErrorCodeEnum.PossibleAnswerIsNull).ToString())
                .WithMessage("PossibleAnswersId cannot be null.");

            When(x => x.PossibleAnswersId != null, () =>
            {
                RuleFor(x => x.PossibleAnswersId)
                .NotEmpty()
                .WithErrorCode(((int)ValidationErrorCodeEnum.PossibleAnswerIsEmpty).ToString())
                .WithMessage("PossibleAnswersId cannot be empty.");
            });

            When(x => !x.PossibleAnswerGroupId.Equals(0), () =>
            {
                RuleForEach(x => x.PossibleAnswersId)
                .MustAsync(async (x, possibleAnswerId, cancellationToken) => await BeValidPossibleAnswerIdAsync(possibleAnswerId, x.PossibleAnswerGroupId))
                .WithErrorCode(((int)ValidationErrorCodeEnum.PossibleAnswerInvalidForQuestion).ToString())
                .WithMessage(x => string.Concat("PossibleAnswersId with id {PropertyValue} ", $"does not belongs to Possible Answer Group with id {x.PossibleAnswerGroupId}."));
            });
            
            #endregion
        }

        private async Task<bool> BeValidMedicalAppointmentIdAsync
        (
            int medicalAppointmentId,
            CancellationToken cancellationToken
        )
        {
            var medicalAppointment = await _unitOfWork.MedicalAppointmentRepository.GetById(medicalAppointmentId);

            if (medicalAppointment != null)
                return true;

            return false;
        }

        private async Task<bool> BeMedicalAppointmentUnfinalizedAsync
        (
            int medicalAppointmentId,
            CancellationToken cancellationToken
        )
        {
            var medicalAppointment = await _unitOfWork.MedicalAppointmentRepository.GetById(medicalAppointmentId);

            if (medicalAppointment.Status.Id.Equals((int)MedicalAppointmentStatusEnum.InProgress))
                return true;

            return false;
        }

        private async Task<bool> BeValidDoctorIdAsync
        (
            int doctorId,
            CancellationToken cancellationToken
        )
        {
            var doctor = await _unitOfWork.DoctorRepository.GetById(doctorId);

            if (doctor != null)
                return true;

            return false;
        }

        private async Task<bool> BeValidQuestionIdAsync
        (
            int questionId,
            CancellationToken cancellationToken
        )
        {
            var question = await _unitOfWork.QuestionRepository.GetById(questionId);

            if (question != null)
                return true;

            return false;
        }

        private async Task<bool> BeCurrentNode
        (
            int medicalAppointmentId,
            int questionId  
        ) 
        {
            var medicalAppointment = await _unitOfWork.MedicalAppointmentRepository.GetById(medicalAppointmentId);

            var question = await _unitOfWork.QuestionRepository.GetById(questionId);

            if (question.Id.Equals(medicalAppointment.CurrentNode.Id))
                return true;

            return false;
        }

        private async Task<bool> BeValidPossibleAnswerGroupIdAsync
        (
            int possibleAnswerGroupId,
            CancellationToken cancellationToken
        )
        {
            var possibleAnswerGroup = await _unitOfWork.PossibleAnswerGroupRepository.GetById(possibleAnswerGroupId);

            if (possibleAnswerGroup != null)
                return true;

            return false;
        }

        private async Task<bool> BeValidPossibleAnswerIdAsync
        (
            int possibleAnswerId,
            int possibleAnswerGroupId
        )
        {
            var possibleAnswer = await _unitOfWork.PossibleAnswerRepository.GetById(possibleAnswerId);

            if (possibleAnswer != null && possibleAnswer.PossibleAnswerGroup.Id.Equals(possibleAnswerGroupId))
                return true;

            return false;
        }

        private async Task<bool> BeCurrenteMedicalAppoinmentNode
        (
            int questionId,
            int medicalAppointmentId,
            CancellationToken cancellationToken
        )
        {
            return !questionId.Equals(0) &&
            await BeValidQuestionIdAsync(questionId, cancellationToken) &&
            await BeValidMedicalAppointmentIdAsync(medicalAppointmentId, cancellationToken) &&
            await BeMedicalAppointmentUnfinalizedAsync(medicalAppointmentId, cancellationToken);
        }

        private async Task<bool> BeUnfinalizedMedicalAppointment
        (
            int medicalAppointmentId,
            CancellationToken cancellationToken
        ) 
        {
            return !medicalAppointmentId.Equals(0) &&
            await BeValidMedicalAppointmentIdAsync(medicalAppointmentId, cancellationToken) &&
            await BeValidMedicalAppointmentIdAsync(medicalAppointmentId, cancellationToken);
        }
    }
}
