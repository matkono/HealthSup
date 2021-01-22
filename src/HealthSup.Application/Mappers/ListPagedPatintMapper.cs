using HealthSup.Application.DataContracts.v1.Responses.Patient;
using HealthSup.Domain.Entities;
using System.Collections.Generic;
using System.Linq;

namespace HealthSup.Application.Mappers
{
    public static class ListPagedPatintMapper
    {
        public static ListPagedPatientsResponse ToDataContract(this List<Patient> patients)
            => new ListPagedPatientsResponse()
            {
                Patients = patients.Select(patient => patient.ToDataContract()).ToList()
            };
    }
}
