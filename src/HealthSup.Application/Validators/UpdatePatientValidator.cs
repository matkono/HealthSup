using FluentValidation;
using HealthSup.Application.DataContracts.v1.Requests.Patient;
using HealthSup.Application.Validators.Contracts;
using HealthSup.Domain.Enums;
using HealthSup.Domain.Repositories;
using System.Threading;
using System.Threading.Tasks;

namespace HealthSup.Application.Validators
{
    public class UpdatePatientValidator : AbstractValidator<UpdatePatientRequest>, IUpdatePatientValidator
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdatePatientValidator
        (
            IUnitOfWork unitOfWork
        )
        {
            _unitOfWork = unitOfWork;

            #region PatientId

            RuleFor(x => x.PatientId)
               .NotEqual(0)
               .WithErrorCode(((int)ValidationErrorCodeEnum.PatientIdIsNull).ToString())
               .WithMessage("PatientId is required, and cannot be 0, to get next node.");

            When(x => !x.PatientId.Equals(0), () =>
            {
                RuleFor(x => x.PatientId)
                .MustAsync(BeValidPatientIdAsync)
                .WithErrorCode(((int)ValidationErrorCodeEnum.PatientNotFound).ToString())
                .WithMessage("Patient with id {PropertyValue} is not found.");
            });

            #endregion

            #region Address

            When(x => x.Address != null, () =>
            {
                RuleFor(x => x.Address.Cep)
                   .Must(BeValidAddressContent)
                   .WithErrorCode(((int)ValidationErrorCodeEnum.PatientAddressCepIsNullOrEmpty).ToString())
                   .WithMessage("Cep address cannot be null or empty.");
            });

            When(x => x.Address != null, () =>
            {
                RuleFor(x => x.Address.City)
                   .Must(BeValidAddressContent)
                   .WithErrorCode(((int)ValidationErrorCodeEnum.PatientAddressCityIsNullOrEmpty).ToString())
                   .WithMessage("City cannot be null or empty.");
            });

            When(x => x.Address != null, () =>
            {
                RuleFor(x => x.Address.Neighborhood)
                   .Must(BeValidAddressContent)
                   .WithErrorCode(((int)ValidationErrorCodeEnum.PatientAddressNeighborhoodIsNullOrEmpty).ToString())
                   .WithMessage("Neighborhood cannot be null or empty.");
            });

            #endregion
        }

        private async Task<bool> BeValidPatientIdAsync(int patientId, CancellationToken cancellationToken)
        {
            var patient = await _unitOfWork.PatientRepository.GetById(patientId);

            if (patient != null)
                return true;

            return false;
        }

        private bool BeValidAddressContent
        (
            string addressContent
        ) 
        {
            return string.IsNullOrEmpty(addressContent) ? false : true;
        }
    }
}
