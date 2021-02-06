using HealthSup.Domain.Repositories;

namespace HealthSup.Application.DataContracts.v1.Requests
{
    public class Pagination
    {
        public int PageSize { get; set; }

        public int PageNumber { get; set; }
    }
}
