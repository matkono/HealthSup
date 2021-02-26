using System.Collections.Generic;

namespace HealthSup.Application.DataContracts.v1.Responses.Disease
{
    public class ListDiseasesPagedResponse
    {
        public List<DiseaseResponse> Diseases { get; set; }

        public int PageNumber { get; set; }

        public int PageSize { get; set; }

        public int TotalRows { get; set; }
    }
}
