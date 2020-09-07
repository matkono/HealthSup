using HealthSup.Domain.Entities;
using HealthSup.Domain.Repositories;
using HealthSup.Domain.Services.Contracts;
using System.Threading.Tasks;

namespace HealthSup.Domain.Services
{
    public class NodeDomainService : INodeDomainService
    {
        public NodeDomainService
        (
            IUnitOfWork unitOfWork
        )
        {
            _unitOfWork = unitOfWork;
        }

        private readonly IUnitOfWork _unitOfWork;

        public async Task<Node> GetInitialByDecisionTreeId
        (
            int decisionTreeId
        )
        {
            var initialNode = await _unitOfWork.NodeRepository.GetInitialByDecisionTreeId
            (
                decisionTreeId
            );

            return initialNode;
        }
    }
}
