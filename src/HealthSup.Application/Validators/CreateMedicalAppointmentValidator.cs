using FluentValidation;
using HealthSup.Application.DataContracts.v1.Requests.MedicalAppointment;
using HealthSup.Application.Validators.Contracts;
using HealthSup.Domain.Enums;
using HealthSup.Domain.Repositories;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace HealthSup.Application.Validators
{
    public class CreateMedicalAppointmentValidator : AbstractValidator<CreateMedicalAppointmentRequest>, ICreateMedicalAppointmentValidator
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateMedicalAppointmentValidator
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

            #region DiseaseId

            RuleFor(x => x.DiseaseId)
               .NotEqual(0)
               .WithErrorCode(((int)ValidationErrorCodeEnum.DiseaseIdIsNull).ToString())
               .WithMessage("DiseaseId is required, and cannot be 0, to get next node.");

            When(x => !x.DiseaseId.Equals(0), () =>
            {
                RuleFor(x => x.DiseaseId)
                .MustAsync(BeValidDiseaseIdAsync)
                .WithErrorCode(((int)ValidationErrorCodeEnum.DiseaseNotFound).ToString())
                .WithMessage("Disease with id {PropertyValue} is not found.");
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

        private async Task<bool> BeValidDiseaseIdAsync(int diseaseId, CancellationToken cancellationToken)
        {
            var disease = await _unitOfWork.DiseaseRepository.GetById(diseaseId);

            if (disease != null)
                return true;

            return false;
        }
    }
}
