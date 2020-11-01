using FluentValidation;
using HealthSup.Application.DataContracts.v1.Requests.Node;
using HealthSup.Application.Validators.Contracts;
using HealthSup.Domain.Repositories;
using System.Threading;
using System.Threading.Tasks;

namespace HealthSup.Application.Validators
{
    public class GetNextNodeValidator: AbstractValidator<GetNextNodeRequest>, IGetNextNodeValidator
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetNextNodeValidator
        (
            IUnitOfWork unitOfWork
        )
        {
            _unitOfWork = unitOfWork;

            RuleFor(x => x.MedicalAppointmentId)
                .MustAsync(BeValidMedicalAppointmentIdAsync)
                .WithMessage("Medical appointment not found.");

            RuleFor(x => x.DoctorId)
                .MustAsync(BeValidDoctorIdAsync)
                .WithMessage("Doctor not found.");

            RuleFor(x => x.QuestionId)
                .MustAsync(BeValidQuestionIdAsync)
                .WithMessage("Question not found.");

            RuleFor(x => x.QuestionId)
                .MustAsync(BeUnansweredQuestionAsync)
                .WithMessage("Question is already answered.");

            RuleFor(x => x.PossibleAnswerGroupId)
                .MustAsync(BeValidPossibleAnswerGroupIdAsync)
                .WithMessage("Possible answer group not found.");

            RuleForEach(x => x.PossibleAnswersId)
                .MustAsync(async (x, possibleAnswerId, cancellationToken) => await BeValidPossibleAnswerIdAsync(possibleAnswerId, x.PossibleAnswerGroupId))
                .WithMessage("PossibleAnswersId does not belongs to Possible Answer Group.");
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

        private async Task<bool> BeUnansweredQuestionAsync
        (
            int questionId,
            CancellationToken cancellationToken
        ) 
        {
            var question = await _unitOfWork.QuestionRepository.GetById(questionId);

            var medicalAppointmentMovement = await _unitOfWork.MedicalAppointmentMovementRepository.GetByFromNodeId(question.Id);

            if (medicalAppointmentMovement == null)
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

            if (possibleAnswer.PossibleAnswerGroup.Id.Equals(possibleAnswerGroupId))
                return true;

            return false;
        }
    }
}
