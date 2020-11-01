﻿using FluentValidation;
using HealthSup.Application.DataContracts.v1.Requests.Node;

namespace HealthSup.Application.Validators.Contracts
{
    public interface IGetNextNodeValidator: IValidator<GetNextNodeRequest>
    {
    }
}
