using HealthSup.Domain.Repositories;

namespace HealthSup.Application.DataContracts.v1.Requests
{
    public class Pagination
    {
        public uint PageSize { get; set; }

        public uint PageNumber { get; set; }
    }
}
