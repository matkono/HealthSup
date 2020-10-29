using FluentValidation;
using HealthSup.Application.DataContracts.v1.Requests.Node;
using HealthSup.Domain.Repositories;

namespace HealthSup.Application.Validators
{
    public class GetNextNodeValidator: AbstractValidator<GetNextNodeRequest>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetNextNodeValidator
        (
            IUnitOfWork unitOfWork
        )
        {
            _unitOfWork = unitOfWork;

            RuleFor(x => x.MedicalAppointmentId)
                .Must(BeValidMedicalAppointmentId)
                .WithMessage("Medical appointment not found.");

            RuleFor(x => x.DoctorId)
                .Must(BeValidDoctorId)
                .WithMessage("Doctor not found.");

            RuleFor(x => x.QuestionId)
                .Must(BeValidQuestionId)
                .WithMessage("Question not found.");

            RuleFor(x => x.QuestionId)
                .Must(BeUnansweredQuestion)
                .WithMessage("Question is already answered.");

            RuleFor(x => x.PossibleAnswerGroupId)
                .Must(BeValidPossibleAnswerGroupId)
                .WithMessage("Possible answer group not found.");

            RuleForEach(x => x.PossibleAnswersId)
                .Must((x, possibleAnswerId) => BeValidPossibleAnswerId(possibleAnswerId, x.PossibleAnswerGroupId))
                .WithMessage("PossibleAnswersId does not belongs to Possible Answer Group.");
        }

        private bool BeValidMedicalAppointmentId
        (
            int medicalAppointmentId
        )
        {
            var medicalAppointment = _unitOfWork.MedicalAppointmentRepository.GetById(medicalAppointmentId).Result;

            if (medicalAppointment != null)
                return true;

            return false;
        }

        private bool BeValidDoctorId
        (
            int doctorId
        )
        {
            var doctor = _unitOfWork.DoctorRepository.GetById(doctorId).Result;

            if (doctor != null)
                return true;

            return false;
        }

        private bool BeValidQuestionId
        (
            int questionId
        )
        {
            var question = _unitOfWork.QuestionRepository.GetById(questionId).Result;

            if (question != null)
                return true;

            return false;
        }

        private bool BeUnansweredQuestion
        (
            int questionId
        ) 
        {
            var question = _unitOfWork.QuestionRepository.GetById(questionId).Result;

            var medicalAppointmentMovement = _unitOfWork.MedicalAppointmentMovementRepository.GetByFromNodeId(question.Id).Result;

            if (medicalAppointmentMovement == null)
                return true;

            return false;
        }

        private bool BeValidPossibleAnswerGroupId
        (
            int possibleAnswerGroupId
        )
        {
            var possibleAnswerGroup = _unitOfWork.PossibleAnswerGroupRepository.GetById(possibleAnswerGroupId);

            if (possibleAnswerGroup != null)
                return true;

            return false;
        }

        private bool BeValidPossibleAnswerId
        (
            int possibleAnswerId,
            int possibleAnswerGroupId
        )
        {
            var possibleAnswer = _unitOfWork.PossibleAnswerRepository.GetById(possibleAnswerId).Result;

            if (possibleAnswer.PossibleAnswerGroup.Id.Equals(possibleAnswerGroupId))
                return true;

            return false;
        }
    }
}
