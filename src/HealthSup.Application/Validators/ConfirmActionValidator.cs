using FluentValidation;
using HealthSup.Application.DataContracts.v1.Requests.DecisionEngine;
using HealthSup.Application.Validators.Contracts;
using HealthSup.Domain.Enums;
using HealthSup.Domain.Repositories;
using System.Threading;
using System.Threading.Tasks;

namespace HealthSup.Application.Validators
{
    public class ConfirmActionValidator : AbstractValidator<ConfirmActionRequest>, IConfirmActionValidator
    {
        private readonly IUnitOfWork _unitOfWork;

        public ConfirmActionValidator
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

            #region ActionId

            RuleFor(x => x.ActionId)
               .NotEqual(0)
               .WithErrorCode(((int)ValidationErrorCodeEnum.ActionIsNull).ToString())
               .WithMessage("ActionId is required, and cannot be 0, to get next node.");

            When(x => !x.ActionId.Equals(0), () =>
            {
                RuleFor(x => x.ActionId)
                .MustAsync(BeValidActionIdAsync)
                .WithErrorCode(((int)ValidationErrorCodeEnum.ActionNotFound).ToString())
                .WithMessage("Action with id {PropertyValue} is not found.");
            });

            WhenAsync(async (x, cancellationToken) => !x.ActionId.Equals(0) &&
            await BeValidActionIdAsync(x.ActionId, cancellationToken), () =>
            {
                RuleFor(x => x.ActionId)
                .MustAsync(async (x, actionId, cancellationToken) => await BeMedicalAppointmentCurrentNode(x.MedicalAppointmentId, actionId, cancellationToken))
                .WithErrorCode(((int)ValidationErrorCodeEnum.ActionNotFound).ToString())
                .WithMessage(x => string.Concat("Action with id {PropertyValue} is not the current node of Medical Appoint", $" with id {x.MedicalAppointmentId}, so cannot be confirmed."));
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

        private async Task<bool> BeValidActionIdAsync
        (
            int actionId,
            CancellationToken cancellationToken
        )
        {
            var action = await _unitOfWork.ActionRepository.GetById(actionId);

            if (action != null)
                return true;

            return false;
        }

        private async Task<bool> BeMedicalAppointmentCurrentNode
        (
            int medicalAppointmentId,
            int actionId,
            CancellationToken cancellationToken
        )
        {
            var medicalAppointment = await _unitOfWork.MedicalAppointmentRepository.GetById(medicalAppointmentId);

            var action = await _unitOfWork.ActionRepository.GetById(actionId);

            if (medicalAppointment.CurrentNode.Id.Equals(action.Id))
                return true;

            return false;
        }
    }
}
