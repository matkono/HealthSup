using HealthSup.Application.DataContracts.v1.Requests;
using HealthSup.Application.DataContracts.v1.Requests.Patient;
using HealthSup.Application.DataContracts.v1.Responses.MedicalAppointment;
using HealthSup.Application.DataContracts.v1.Responses.Patient;
using HealthSup.Application.Mappers;
using HealthSup.Application.Services.Contracts;
using HealthSup.Application.Validators.Contracts;
using HealthSup.Domain.Repositories;
using HealthSup.Domain.Services.Contracts;
using HealthSup.Infrastructure.CrossCutting.Constants;
using System;
using System.Threading.Tasks;

namespace HealthSup.Application.Services
{
    public class PatientApplicationService : IPatientApplicationService
    {
        public PatientApplicationService
        (
            IUnitOfWork unitOfWork,
            IPatientDomainService patientDomainService,
            ICreatePatientValidator createPatientValidator
        )
        {
            _unitOfWork = unitOfWork;
            PatientDomainService = patientDomainService ?? throw new ArgumentNullException(nameof(patientDomainService));
            CreatePatientValidator = createPatientValidator;
        }

        private readonly IUnitOfWork _unitOfWork;
        private readonly IPatientDomainService PatientDomainService;
        private readonly ICreatePatientValidator CreatePatientValidator;

        public async Task<ListPatientsPagedReturn> ListPaged(Pagination pagination)
        {
            var pageNumber = pagination.PageNumber;
            var pageSize = pagination.PageSize > ApplicationConstants.MaxPageSize ? ApplicationConstants.MaxPageSize : pagination.PageSize;

            var patients = await _unitOfWork.PatientRepository.ListPaged(pageNumber, pageSize);

            return new ListPatientsPagedReturn(patients.ToDataContract());
        }

        public async Task<GetPatientByRegistrationReturn> GetByRegistration
        (
            string registration
        )
        {
            var patient = await _unitOfWork.PatientRepository.GetByRegistration(registration);

            return new GetPatientByRegistrationReturn(patient.ToDataContract());
        }

        public async Task<ListMedicalAppointmentsPagedByPatientIdReturn> ListMedicalAppointments
        (
            int patientId,
            Pagination pagination
        )
        {
            var pageNumber = pagination.PageNumber;
            var pageSize = pagination.PageSize > ApplicationConstants.MaxPageSize ? ApplicationConstants.MaxPageSize : pagination.PageSize;

            var medicalAppointments = await _unitOfWork.MedicalAppointmentRepository.ListPagedByPatientId(patientId, pageNumber, pageSize);

            return new ListMedicalAppointmentsPagedByPatientIdReturn(medicalAppointments.ToDataContract());
        }

        public async Task<CreatePatientReturn> Create
        (
            CreatePatientRequest argument
        )
        {
            var resultValidator = CreatePatientValidator.Validate(argument);

            if (!resultValidator.IsValid)
            {
                var response = new CreatePatientReturn(null);

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

            var patientModel = argument.Patient.ToModel();

            _unitOfWork.Begin();
            try
            {
                var patient = await PatientDomainService.Create(patientModel);

                _unitOfWork.Commit();

                return new CreatePatientReturn(patient.ToDataContract());
            }
            catch(Exception e)
            {
                _unitOfWork.Rollback();
                throw e;
            }
        }
    }
}
