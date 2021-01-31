using HealthSup.Application.DataContracts.v1.Responses.Patient;
using HealthSup.Domain.Entities;
using System.Collections.Generic;
using System.Linq;

namespace HealthSup.Application.Mappers
{
    public static class ListPagedPatintMapper
    {
        public static ListPagedResponse ToDataContract(this PagedResult<List<Patient>> patients)
            => new ListPagedResponse()
            {
                Patients = patients.Data.Select(patient => patient.ToDataContract()).ToList(),
                PageNumber = patients.PageNumber,
                PageSize = patients.PageSize,
                TotalRows = patients.TotalRows
            };
    }
}
