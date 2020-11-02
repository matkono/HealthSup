using HealthSup.Domain.Entities;
using HealthSup.Domain.Repositories;
using HealthSup.Domain.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HealthSup.Domain.Services
{
    public class DecisionEngineDomainService : IDecisionEngineDomainService
    {
        public DecisionEngineDomainService
        (
            IUnitOfWork unitOfWork
        )
        {
            _unitOfWork = unitOfWork;
        }

        private readonly IUnitOfWork _unitOfWork;
    
    public Task<Node> ResolveNextNode
        (
            int medicalAppointmentId, 
            int doctorId, 
            int questionId, 
            int PossibleAnswerGroupId, 
            DateTime date, 
            List<int> PossibleAnswersId
        )
        {
            throw new NotImplementedException();
        }
    }
}
