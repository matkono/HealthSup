using Dapper;
using HealthSup.Domain.Entities;
using HealthSup.Domain.Repositories;
using HealthSup.Infrastructure.Data.Scripts;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthSup.Infrastructure.Data.Repositories
{
    public class PatientRepository : IPatientRepository
    {
        public PatientRepository
        (
            IUnitOfWork unitOfWork
        )
        {
            UnitOfWork = unitOfWork;
        }

        private IUnitOfWork UnitOfWork { get; }

        public async Task<PagedResult<List<Patient>>> ListPaged
        (
            int pageNumber,
            int pageSize
        )
        {
            Patient MapFromQuery
            (
                Patient patient,
                Address address
            )
            {
                patient.SetAddress(address);

                return patient;
            };

            var listQuery = ScriptManager.GetByName(ScriptManager.FileNames.Patient.ListPagedPatients);
            var countQuery = ScriptManager.GetByName(ScriptManager.FileNames.Patient.CountPatients);

            var result = await UnitOfWork.Connection.QueryAsync<Patient, Address, Patient>(
                                                                listQuery,
                                                                MapFromQuery,
                                                                new { pageNumber, pageSize },
                                                                UnitOfWork.Transaction);

            var count = UnitOfWork.Connection.ExecuteScalar<int>(countQuery, UnitOfWork.Transaction);

            var toReturn = new PagedResult<List<Patient>>(result.ToList(), pageNumber, pageSize, count);

            return toReturn;
        }

        public async Task<Patient> GetById
        (
            int id
        )
        {
            Patient MapFromQuery
               (
                   Patient patient,
                   Address address
               )
            {
                patient.SetAddress(address);

                return patient;
            };

            var query = ScriptManager.GetByName(ScriptManager.FileNames.Patient.GetById);

            var result = await UnitOfWork.Connection.QueryAsync<Patient, Address, Patient>(
                                                                query,
                                                                MapFromQuery,
                                                                new { id },
                                                                UnitOfWork.Transaction);

            return result.FirstOrDefault();
        }

        public async Task<Patient> GetByRegistration
        (
            string registration
        )
        {
            Patient MapFromQuery
            (
                Patient patient,
                Address address
            )
            {
                patient.SetAddress(address);

                return patient;
            };

            var query = ScriptManager.GetByName(ScriptManager.FileNames.Patient.GetByRegistration);

            var result = await UnitOfWork.Connection.QueryAsync<Patient, Address, Patient>(
                                                                query,
                                                                MapFromQuery,
                                                                new { registration },
                                                                UnitOfWork.Transaction);

            return result.FirstOrDefault();
        }

        public async Task<Patient> Create
        (
            Patient patient
        )
        {
            var query = ScriptManager.GetByName(ScriptManager.FileNames.Patient.Create);
            var parameters = new DynamicParameters();
            parameters.Add("@name", patient.Name);
            parameters.Add("@registration", patient.Registration);
            parameters.Add("@addressId", patient.Address.Id);

            var result = await UnitOfWork.Connection.QueryAsync<int>(query,
                                                      parameters,
                                                      UnitOfWork.Transaction);

            return await GetById(result.Single());
        }

        public async Task<Patient> Update
        (
            Patient patient
        )
        {
            var query = new StringBuilder();
            query.Append(ScriptManager.GetByName(ScriptManager.FileNames.Patient.Update));

            var valuesToBeUpdated = new StringBuilder();
            var parameters = new DynamicParameters();
            parameters.Add("@id", patient.Id);

            if (patient.Address != null) 
            {
                valuesToBeUpdated.Append("addressId = @addressId");
                parameters.Add("@addressId", patient.Address.Id);
            }

            if(string.IsNullOrEmpty(valuesToBeUpdated.ToString()))
                return await GetById(patient.Id);

            query.Replace("@valuesToBeUpdated", valuesToBeUpdated.ToString());

            await UnitOfWork.Connection.QueryAsync<int>(query.ToString(),
                                                        parameters,
                                                        UnitOfWork.Transaction);

            return await GetById(patient.Id);
        }
    }
}
