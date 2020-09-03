using System.Collections.Generic;

namespace HealthSup.Domain.Entities
{
    public class BaseEntity
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

    public class BaseEntity<T> : BaseEntity
    {
        public BaseEntity
        (
            T data
        )
        {
            Data = data;
        }

        public T Data { get; set; }
    }
}
