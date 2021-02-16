using HealthSup.Application.DataContracts.v1.Requests.MedicalAppointment;
using HealthSup.Application.DataContracts.v1.Responses.MedicalAppointment;
using HealthSup.Application.Mappers;
using HealthSup.Application.Services.Contracts;
using HealthSup.Application.Validators.Contracts;
using HealthSup.Domain.Entities;
using HealthSup.Domain.Enums;
using HealthSup.Domain.Repositories;
using HealthSup.Domain.Services.Contracts;
using HealthSup.Infrastructure.CrossCutting.Constants;
using System;
using System.Threading.Tasks;
using Action = HealthSup.Domain.Entities.Action;

namespace HealthSup.Application.Services
{
    public class MedicalAppointmentApplicationService : IMedicalAppointmentApplicationService
    {
        public MedicalAppointmentApplicationService
        (
            IMedicalAppointmentDomainService medicalAppointmentService,
            INodeDomainService nodeService,
            IUnitOfWork unitOfWork,
            ICreateMedicalAppointmentValidator createMedicalAppointmentValidator
        )
        {
            MedicalAppointmentService = medicalAppointmentService ?? throw new ArgumentNullException(nameof(medicalAppointmentService));
            NodeService = nodeService ?? throw new ArgumentNullException(nameof(nodeService));
            _unitOfWork = unitOfWork;
            CreateMedicalAppointmentValidator = createMedicalAppointmentValidator;
        }

        private readonly IMedicalAppointmentDomainService MedicalAppointmentService;

        private readonly INodeDomainService NodeService;

        private readonly IUnitOfWork _unitOfWork;

        private readonly ICreateMedicalAppointmentValidator CreateMedicalAppointmentValidator;

        public async Task<GetMedicalAppointmentLastNodeReturn> GetLastNode
        (
            int medicalAppointmentId
        )
        {
            var medicalAppointment = await MedicalAppointmentService.GetById(medicalAppointmentId);

            if (medicalAppointment == null)
            {
                var response = new GetMedicalAppointmentLastNodeReturn(null);

                response.AddError
                (
                    (int)ValidationErrorCodeEnum.MedicalAppointNotFound,
                    "Medical appointment not found.",
                    null
                );

                return response;
            }

            var node = await NodeService.GetCurrentNode(medicalAppointment);

            if (node is Action action)
            {
                return new GetMedicalAppointmentLastNodeReturn(action.ToDataContract());
            }
            else if (node is Question question)
            {
                return new GetMedicalAppointmentLastNodeReturn(question.ToDataContract());
            }
            else
            {
                var decision = node as Decision;
                return new GetMedicalAppointmentLastNodeReturn(decision.ToDataContract());
            }
        }
          
        public async Task<ListMedicalAppointmentsPagedByPatientIdReturn> ListPaged
        (
            ListMedicalAppointmentPagedByPatientIdRequest argument
        )
        {
            var pageNumber = argument.Pagination.PageNumber;
            var pageSize = argument.Pagination.PageSize > ApplicationConstants.MaxPageSize ? ApplicationConstants.MaxPageSize : argument.Pagination.PageSize;

            var medicalAppointments = await _unitOfWork.MedicalAppointmentRepository.ListPagedByPatientId(argument.PatientId, pageNumber, pageSize);

            return new ListMedicalAppointmentsPagedByPatientIdReturn(medicalAppointments.ToDataContract());
        }

        public async Task<CreateMedicalAppointmentReturn> Create
        (
            CreateMedicalAppointmentRequest argument
        )
        {
            var resultValidator = await CreateMedicalAppointmentValidator.ValidateAsync(argument);

            if (!resultValidator.IsValid)
            {
                var response = new CreateMedicalAppointmentReturn(null);

                foreach (var error in resultValidator.Errors)
                {
                    response.AddError
                    (
                        Int32.Parse(error.ErrorCode),
                        error.ErrorMessage,
                        error.PropertyName
                    );
                }

                return response;
            }

            var medicalAppointment = await MedicalAppointmentService.Create(argument.PatientId, argument.DiseaseId);

            return new CreateMedicalAppointmentReturn(medicalAppointment.ToDataContract());
        }
    }
}
