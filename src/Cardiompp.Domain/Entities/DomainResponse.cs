using System.Collections.Generic;

namespace Cardiompp.Domain.Entities
{
    public class DomainResponse
    {
        public IList<ErrorResponse> Errors { get; set; }

        public void AddError
        (
            int code,
            string message,
            string field
        )
        {
            if (Errors == null)
                Errors = new List<ErrorResponse>();

            var error = new ErrorResponse()
            {
                Code = code,
                Message = message,
                Field = field
            };

            Errors.Add(error);
        }
    }

    public class DomainResponse<T> : DomainResponse
    {
        public DomainResponse(T data)
        {
            Data = data;
        }

        public T Data { get; set; }
    }
}
