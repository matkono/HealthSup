using FluentValidation;
using HealthSup.Application.DataContracts.v1.Requests.Patient;

namespace HealthSup.Application.Validators.Contracts
{
    public interface ICreatePatientValidator : IValidator<CreatePatientRequest>
    {
    }
}
