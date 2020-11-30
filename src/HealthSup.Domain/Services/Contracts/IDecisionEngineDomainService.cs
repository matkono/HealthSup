using HealthSup.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HealthSup.Domain.Services.Contracts
{
    public interface IDecisionEngineDomainService
    {
        Task<Node> ResolveNextNode
        (
            int medicalAppointmentId, 
            int doctorId,
            int questionId,
            int PossibleAnswerGroupId,
            DateTime date,
            List<int> PossibleAnswersId
        );

        Task ConfirmAction
        (
            int medicalAppointmentId
        );
    }
}
