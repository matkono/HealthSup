using HealthSup.Application.DataContracts.v1.Responses.Disease;
using HealthSup.Domain.Entities;
using System.Collections.Generic;
using System.Linq;

namespace HealthSup.Application.Mappers
{
    public static class ListPagedDiseaseMapper
    {
        public static ListDiseasesPagedResponse ToDataContract(this PagedResult<List<Disease>> diseases)
            => new ListDiseasesPagedResponse()
            {
                Patients = diseases.Data.Select(disease => disease.ToDataContract()).ToList(),
                PageNumber = diseases.PageNumber,
                PageSize = diseases.PageSize,
                TotalRows = diseases.TotalRows
            };
    }
}
