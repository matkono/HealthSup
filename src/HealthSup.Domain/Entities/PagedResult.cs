namespace HealthSup.Domain.Entities
{
    public class PagedResult<T>
    {
        public int PageNumber { get; }

        public int PageSize { get; }

        public int TotalRows { get; }

        public T Data { get; }

        public PagedResult
        (
            T data, 
            int pageNumber, 
            int pageSize, 
            int totalRows
        )
        {
            Data = data;
            PageNumber = pageNumber;
            PageSize = pageSize;
            TotalRows = totalRows;
        }
    }
}
