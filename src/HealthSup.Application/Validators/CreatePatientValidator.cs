using FluentValidation;
using HealthSup.Application.DataContracts.v1.Requests.Patient;
using HealthSup.Application.Validators.Contracts;
using HealthSup.Domain.Enums;
using HealthSup.Infrastructure.CrossCutting.Constants;

namespace HealthSup.Application.Validators
{
    public class CreatePatientValidator : AbstractValidator<CreatePatientRequest>, ICreatePatientValidator
    {
        public CreatePatientValidator() 
        {
            RuleFor(x => x.Patient)
               .NotNull()
               .NotEmpty()
               .WithErrorCode(((int)ValidationErrorCodeEnum.PatientIsNullOrEmpty).ToString())
               .WithMessage("Patient cannot be null or empty.");

            When(x => x.Patient != null, () => 
            {
                RuleFor(x => x.Patient.Name)
                    .NotNull()
                    .NotEmpty()
                    .WithErrorCode(((int)ValidationErrorCodeEnum.PatientNameIsNullOrEmpty).ToString())
                    .WithMessage("Patient's name cannot be null or empty.");
            });

            When(x => x.Patient != null, () =>
            {
                RuleFor(x => x.Patient.Registration)
                    .NotNull()
                    .NotEmpty()
                    .WithErrorCode(((int)ValidationErrorCodeEnum.PatientRegistrationIsNullOrEmpty).ToString())
                    .WithMessage("Patient's registration cannot be null or empty.");
            });

            When(x => x.Patient.Registration != null , () =>
            {
                RuleFor(x => x.Patient.Registration)
                   .Must(x => x.Length == ApplicationConstants.RegistrationSize)
                   .WithErrorCode(((int)ValidationErrorCodeEnum.PatientRegistrationIsNullOrEmpty).ToString())
                   .WithMessage("Patient's registration must have 8 digits.");
            });

            When(x => x.Patient != null, () =>
            {
                RuleFor(x => x.Patient.Address)
                   .NotNull()
                   .NotEmpty()
                   .WithErrorCode(((int)ValidationErrorCodeEnum.PatientAddressIsNullOrEmpty).ToString())
                   .WithMessage("Patient's Address cannot be null or empty.");
            });

            When(x => x.Patient.Address != null, () =>
            {
                RuleFor(x => x.Patient.Address.Cep)
                   .Must(BeValidAddressContent)
                   .WithErrorCode(((int)ValidationErrorCodeEnum.PatientAddressCepIsNullOrEmpty).ToString())
                   .WithMessage("Patient's cep address cannot be null or empty.");
            });

            When(x => x.Patient.Address != null, () =>
            {
                RuleFor(x => x.Patient.Address.City)
                   .Must(BeValidAddressContent)
                   .WithErrorCode(((int)ValidationErrorCodeEnum.PatientAddressCityIsNullOrEmpty).ToString())
                   .WithMessage("Patient's city cannot be null or empty.");
            });

            When(x => x.Patient.Address != null, () =>
            {
                RuleFor(x => x.Patient.Address.Neighborhood)
                   .Must(BeValidAddressContent)
                   .WithErrorCode(((int)ValidationErrorCodeEnum.PatientAddressNeighborhoodIsNullOrEmpty).ToString())
                   .WithMessage("Patient's neighborhood cannot be null or empty.");
            });
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
