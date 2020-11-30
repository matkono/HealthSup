using FluentValidation;
using HealthSup.Application.DataContracts.v1.Requests.DecisionEngine;

namespace HealthSup.Application.Validators.Contracts
{
    public interface IGetPreviousNodeValidator : IValidator<GetPreviousNodeRequest>
    {
    }
}
