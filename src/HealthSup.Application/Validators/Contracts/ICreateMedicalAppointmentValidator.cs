using FluentValidation;
using HealthSup.Application.DataContracts.v1.Requests.MedicalAppointment;

namespace HealthSup.Application.Validators.Contracts
{
    public interface ICreateMedicalAppointmentValidator : IValidator<CreateMedicalAppointmentRequest>
    {
    }
}
