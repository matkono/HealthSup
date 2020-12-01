using HealthSup.Domain.Entities;
using HealthSup.Domain.Enums;
using HealthSup.Domain.Exception;
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

        public async Task<Node> ResolveNextNode
        (
            int medicalAppointmentId,
            int doctorId,
            int questionId,
            int possibleAnswerGroupId,
            DateTime date,
            List<int> PossibleAnswersId
        )
        {
            var question = await _unitOfWork.QuestionRepository.GetById(questionId);
            var decisionTreeRule = await _unitOfWork.DecisionTreeRuleRepository.GetByFromNodeIdAndPossibleAnswerIdAsync(question.Id, possibleAnswerGroupId);
            var medicalAppointment = new MedicalAppointment() { Id = medicalAppointmentId };

            var answers = new List<Answer>();

            foreach (var possibleAnswerId in PossibleAnswersId) 
            {
                answers.Add(new Answer() 
                {
                    Date = date,
                    Question = question,
                    PossibleAnswer = new PossibleAnswer() { Id = possibleAnswerId },
                    Doctor = new Doctor() { Id = doctorId },
                    MedicalAppointment = new MedicalAppointment() { Id = medicalAppointmentId }
                });
            }

            var medicalAppointmentMoviment = new MedicalAppointmentMovement() 
            {
                FromNode = decisionTreeRule.FromNode,
                ToNode = decisionTreeRule.ToNode,
                MedicalAppointment = medicalAppointment
            };

            await _unitOfWork.AnswerRepository.InsertManyAsync(answers);
            await _unitOfWork.MedicalAppointmentMovementRepository.InsetMovement(medicalAppointmentMoviment);
            await _unitOfWork.MedicalAppointmentRepository.UpdateLastNode(medicalAppointment.Id, decisionTreeRule.ToNode.Id);

            var node = await _unitOfWork.NodeRepository.GetById(decisionTreeRule.ToNode.Id);

            return await LoadNodeDetails(node.Id, node.NodeType.Id);
        }

        public async Task<Node> ResolvePreviousNode
        (
            int medicalAppointmentId, 
            int nodeId
        )
        {
            var currentNode = await _unitOfWork.NodeRepository.GetById(nodeId);
            var rule = await _unitOfWork.DecisionTreeRuleRepository.GetByToNodeIdAsync(currentNode.Id);
            var previousNode = await _unitOfWork.NodeRepository.GetById(rule.FromNode.Id);

            if (previousNode.NodeType.Id.Equals((int)NodeTypeEnum.Question)) 
            {
                var question = await _unitOfWork.QuestionRepository.GetByNodeId(previousNode.Id);
                var answers = await _unitOfWork.AnswerRepository.ListByQuestionId(question.Id);
                await _unitOfWork.AnswerRepository.DeleteMany(answers);
            }

            var medicalAppointmentMovement = await _unitOfWork.MedicalAppointmentMovementRepository.GetByToNodeId(currentNode.Id);
            await _unitOfWork.MedicalAppointmentMovementRepository.DeleteById(medicalAppointmentMovement.Id);

            await _unitOfWork.MedicalAppointmentRepository.UpdateLastNode(medicalAppointmentId, previousNode.Id);

            return await LoadNodeDetails(previousNode.Id, previousNode.NodeType.Id);
        }

        public async Task<Node> ConfirmAction
        (
            int medicalAppointmentId
        )
        {
            var medicalAppointment = await _unitOfWork.MedicalAppointmentRepository.GetById(medicalAppointmentId);

            var decisionTreeRule = await _unitOfWork.DecisionTreeRuleRepository.GetActionConfirmationQuestionByNodeId(medicalAppointment.CurrentNode.Id);

            var node = await _unitOfWork.NodeRepository.GetById(decisionTreeRule.ToNode.Id);
            await _unitOfWork.MedicalAppointmentRepository.UpdateLastNode(medicalAppointment.Id, node.Id);
            var medicalAppointmentMoviment = new MedicalAppointmentMovement()
            {
                FromNode = decisionTreeRule.FromNode,
                ToNode = decisionTreeRule.ToNode,
                MedicalAppointment = medicalAppointment
            };
            await _unitOfWork.MedicalAppointmentMovementRepository.InsetMovement(medicalAppointmentMoviment);

            return await LoadNodeDetails(node.Id, node.NodeType.Id);
        }

        public async Task ConfirmDecision
        (
            int medicalAppointmentId,
            int decisionId
        )
        {
            var decision = await _unitOfWork.DecisionRepository.GetById(decisionId);

            if (decision.IsDiagnostic)
                await _unitOfWork.MedicalAppointmentRepository.UpdateIsDiagnostic(medicalAppointmentId, true);

            await _unitOfWork.MedicalAppointmentRepository.UpdateStatus(medicalAppointmentId, (int)MedicalAppointmentStatusEnum.Finalized);
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

                case (int)NodeTypeEnum.Decision:
                    var decision = await _unitOfWork.DecisionRepository.GetByNodeId(nodeId);
                    return decision;

                default:
                    throw new InvalidNodeTypeException("NodeType is invalid.");
            }
        }
    }
}
