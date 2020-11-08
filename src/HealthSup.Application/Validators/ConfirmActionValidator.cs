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

            WhenAsync(async (x, cancellationToken) => await BeValidMedicalAppointmentIdAsync(x.MedicalAppointmentId, cancellationToken) && 
            await BeMedicalAppointmentUnfinalizedAsync(x.MedicalAppointmentId, cancellationToken), () =>
            {
                RuleFor(x => x.MedicalAppointmentId)
                .MustAsync(CanConfirmAction)
                .WithErrorCode(((int)ValidationErrorCodeEnum.MedicalAppointmentCurrentNodeIsNotAction).ToString())
                .WithMessage("Cannot confirm action because the current node of medical appointment with id {PropertyValue} is not action.");
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

        private async Task<bool> MedicalAppointmentExistOnDataBase
        (
            int medicalAppointmentId,
            CancellationToken cancellationToken
        )
        {
            return !medicalAppointmentId.Equals(0) &&
            await BeValidMedicalAppointmentIdAsync(medicalAppointmentId, cancellationToken);
        }

        private async Task<bool> BeValidMedicalAppointment
        (
            int medicalAppointmentId,
            CancellationToken cancellationToken
        )
        {
            return !medicalAppointmentId.Equals(0) &&
            await BeValidMedicalAppointmentIdAsync(medicalAppointmentId, cancellationToken) &&
            await MedicalAppointmentExistOnDataBase(medicalAppointmentId, cancellationToken);
        }

        private async Task<bool> CanConfirmAction
        (
            int medicalAppointmentId,
            CancellationToken cancellationToken
        ) 
        {
            var medicalAppointment = await _unitOfWork.MedicalAppointmentRepository.GetById(medicalAppointmentId);

            var node = await _unitOfWork.NodeRepository.GetById(medicalAppointment.CurrentNode.Id);

            if (node.NodeType.Id.Equals((int)NodeTypeEnum.Action))
                return true;

            return false;
        }
    }
}
