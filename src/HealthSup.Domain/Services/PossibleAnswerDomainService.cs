using HealthSup.Domain.Entities;
using HealthSup.Domain.Repositories;
using HealthSup.Domain.Services.Contracts;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HealthSup.Domain.Services
{
    public class PossibleAnswerDomainService : IPossibleAnswerDomainService
    {
        public PossibleAnswerDomainService
        (
            IUnitOfWork unitOfWork
        )
        {
            _unitOfWork = unitOfWork;
        }

        private readonly IUnitOfWork _unitOfWork;

        public async Task<List<PossibleAnswer>> ListByQuestionId(int questionId)
        {
            return await _unitOfWork.PossibleAnswerRepository.ListByQuestionId(questionId);
        }
    }
}
