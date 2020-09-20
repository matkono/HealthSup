using HealthSup.Domain.Entities;
using HealthSup.Domain.Enums;
using HealthSup.Domain.Exception;
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

            return await LoadInitialNodeDetailsAsync(initialNode.Id, initialNode.NodeType.Id);
        }

        public async Task<Node> ResolveById
        (
            int id
        )
        {
            var node = await _unitOfWork.NodeRepository.GetById(id);

            if (node.NodeType.Id == (int)NodeTypeEnum.Action)
            {
                var decisionTreeRule = await _unitOfWork.DecisionTreeRuleRepository.GetActionConfirmationQuestionByNodeId(node.Id);
                node = await _unitOfWork.NodeRepository.GetById(decisionTreeRule.ToNode.Id);
            }

            return await LoadNodeDetails(node.Id, node.NodeType.Id);
        }

        private async Task<Node> LoadInitialNodeDetailsAsync
        (
            int nodeId,
            int nodeTypeId
        ) 
        {
            switch (nodeTypeId)
            {
                case (int)NodeTypeEnum.Action:
                    var action = await _unitOfWork.ActionRepository.GetByNodeId(nodeId);
                    return action;

                case (int)NodeTypeEnum.Question:
                    var question = await _unitOfWork.QuestionRepository.GetByNodeId(nodeId);
                    question.SetPossibleAnswers(await _unitOfWork.PossibleAnswerRepository.ListByQuestionId(question.Id));
                    return question;

                default:
                    throw new InvalidInitialNodeTypeException("NodeTypeId is invalid to be initial node.");
            }
        }

        private async Task<Node> LoadNodeDetails
        (
            int nodeId,
            int nodeTypeId
        )
        {
            switch (nodeTypeId)
            {
                case (int)NodeTypeEnum.Action:
                    var action = await _unitOfWork.ActionRepository.GetByNodeId(nodeId);
                    return action;

                case (int)NodeTypeEnum.Question:
                    var question = await _unitOfWork.QuestionRepository.GetByNodeId(nodeId);
                    question.SetPossibleAnswers(await _unitOfWork.PossibleAnswerRepository.ListByQuestionId(question.Id));
                    return question;

                default:
                    throw new InvalidNodeTypeException("NodeType is invalid.");
            }
        }
    }
}
