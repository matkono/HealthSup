using FluentValidation;
using HealthSup.Application.DataContracts.v1.Requests.DecisionEngine;
using HealthSup.Application.Validators.Contracts;
using HealthSup.Domain.Enums;
using HealthSup.Domain.Repositories;
using System;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using System.Threading.Tasks;

namespace HealthSup.Application.Validators
{
    public class GetPreviousNodeValidator : AbstractValidator<GetPreviousNodeRequest>, IGetPreviousNodeValidator
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetPreviousNodeValidator
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

            #region CurrentNodeId

            RuleFor(x => x.CurrentNodeId)
                .NotEqual(0)
                .WithErrorCode(((int)ValidationErrorCodeEnum.NodeIdIsNull).ToString())
                .WithMessage("CurrentNodeId is required, and cannot be 0, to get next node.");

            When(x => !x.CurrentNodeId.Equals(0), () =>
            {
                RuleFor(x => x.CurrentNodeId)
                .MustAsync(BeValidNodeIdAsync)
                .WithErrorCode(((int)ValidationErrorCodeEnum.NodeIdIsNotFound).ToString())
                .WithMessage("Node with id {PropertyValue} is not found.");
            });

            WhenAsync(async (x, cancellationToken) => !x.CurrentNodeId.Equals(0) && await BeValidNodeIdAsync(x.CurrentNodeId, cancellationToken), () => 
            {
                RuleFor(x => x.CurrentNodeId)
                .MustAsync(async (x, y, cancellationToken) => await BeNotInitialNode(y, x.MedicalAppointmentId ,cancellationToken))
                .WithErrorCode(((int)ValidationErrorCodeEnum.NodeIdIsNotFound).ToString())
                .WithMessage("Node with id {PropertyValue} is initial, so cannot get the previous node.");
            });

            WhenAsync((x, cancellationToken) => BeValidNodeIdAndMedicalAppointment(x.CurrentNodeId, x.MedicalAppointmentId, cancellationToken), () =>
            {
                RuleFor(x => x.CurrentNodeId)
                .MustAsync(async (x, questionId, cancellationToken) => await BeCurrentNode(x.MedicalAppointmentId, questionId))
                .WithErrorCode(((int)ValidationErrorCodeEnum.ActionIsNotCurrentNode).ToString())
                .WithMessage(x => string.Concat("Node with id {PropertyValue} is not the current node of Medical Appoint", $" with id {x.MedicalAppointmentId}."));
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

        private async Task<bool> BeValidNodeIdAsync
        (
            int currentNodeId,
            CancellationToken cancellationToken
        )
        {
            var node = await _unitOfWork.NodeRepository.GetById(currentNodeId);

            if (node != null)
                return true;

            return false;
        }

        private async Task<bool> BeNotInitialNode
        (
            int currentNodeId,
            int medicalAppointmentId,
            CancellationToken cancellationToken
        )
        {
            var medicalAppointment = await _unitOfWork.MedicalAppointmentRepository.GetById(medicalAppointmentId);
            var initialNode = await _unitOfWork.NodeRepository.GetInitialByDecisionTreeId(medicalAppointment.DecisionTree.Id);
            
            var node = await _unitOfWork.NodeRepository.GetById(currentNodeId);

            if (!node.Id.Equals(initialNode.Id))
                return true;

            return false;
        }

        private async Task<bool> BeValidNodeIdAndMedicalAppointment
        (
            int nodeId,
            int medicalAppointmentId,
            CancellationToken cancellationToken
        )
        {
            return !nodeId.Equals(0) &&
            await BeValidNodeIdAsync(nodeId, cancellationToken) &&
            await BeValidMedicalAppointmentIdAsync(medicalAppointmentId, cancellationToken) &&
            await BeMedicalAppointmentUnfinalizedAsync(medicalAppointmentId, cancellationToken);
        }

        private async Task<bool> BeCurrentNode
        (
            int medicalAppointmentId,
            int nodeId
        )
        {
            var medicalAppointment = await _unitOfWork.MedicalAppointmentRepository.GetById(medicalAppointmentId);

            var node = await _unitOfWork.NodeRepository.GetById(nodeId);

            if (node.Id.Equals(medicalAppointment.CurrentNode.Id))
                return true;

            return false;
        }
    }
}
