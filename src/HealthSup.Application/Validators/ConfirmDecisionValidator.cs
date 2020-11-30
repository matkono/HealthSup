using FluentValidation;
using HealthSup.Application.DataContracts.v1.Requests.DecisionEngine;
using HealthSup.Application.Validators.Contracts;
using HealthSup.Domain.Enums;
using HealthSup.Domain.Repositories;
using System.Threading;
using System.Threading.Tasks;

namespace HealthSup.Application.Validators
{
    public class ConfirmDecisionValidator : AbstractValidator<ConfirmDecisionRequest>, IConfirmDecisionValidator
    {
        private readonly IUnitOfWork _unitOfWork;

        public ConfirmDecisionValidator
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

            WhenAsync((x, cancellationToken) => BeValidMedicalAppointmentIdAsync(x.MedicalAppointmentId, cancellationToken), () =>
            {
                RuleFor(x => x.MedicalAppointmentId)
                .MustAsync(BeMedicalAppointmentUnfinalizedAsync)
                .WithErrorCode(((int)ValidationErrorCodeEnum.MedicalAppointmentIsFinalized).ToString())
                .WithMessage("Medical appointment with id {PropertyValue} is finalized.");
            });

            #endregion

            #region DecisionId

            RuleFor(x => x.DecisionId)
               .NotEqual(0)
               .WithErrorCode(((int)ValidationErrorCodeEnum.DecisionIsNull).ToString())
               .WithMessage("DecisionId is required, and cannot be 0, to get next node.");

            When(x => !x.DecisionId.Equals(0), () =>
            {
                RuleFor(x => x.DecisionId)
                .MustAsync(BeValidDecisionIdAsync)
                .WithErrorCode(((int)ValidationErrorCodeEnum.DecisionNotFound).ToString())
                .WithMessage("Decision with id {PropertyValue} is not found.");
            });

            WhenAsync(async (x, cancellationToken) => !x.DecisionId.Equals(0) &&
            await BeValidDecisionIdAsync(x.DecisionId, cancellationToken), () =>
            {
                RuleFor(x => x.DecisionId)
                .MustAsync(async (x, decisionId, cancellationToken) => await BeMedicalAppointmentCurrentNode(x.MedicalAppointmentId, decisionId, cancellationToken))
                .WithErrorCode(((int)ValidationErrorCodeEnum.DecisionIsNotCurrentNode).ToString())
                .WithMessage(x => string.Concat("Decision with id {PropertyValue} is not the current node of Medical Appoint", $" with id {x.MedicalAppointmentId}, so cannot be confirmed."));
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

        private async Task<bool> BeValidDecisionIdAsync
        (
            int decisionId,
            CancellationToken cancellationToken
        )
        {
            var decision = await _unitOfWork.DecisionRepository.GetById(decisionId);

            if (decision != null)
                return true;

            return false;
        }

        private async Task<bool> BeMedicalAppointmentCurrentNode
        (
            int medicalAppointmentId,
            int decisionId,
            CancellationToken cancellationToken
        )
        {
            var medicalAppointment = await _unitOfWork.MedicalAppointmentRepository.GetById(medicalAppointmentId);

            var decision = await _unitOfWork.DecisionRepository.GetById(decisionId);

            if (medicalAppointment.CurrentNode.Id.Equals(decision.Id))
                return true;

            return false;
        }
    }
}
