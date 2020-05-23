using System.Collections.Generic;

namespace Cardiompp.Application.DataContracts.Responses
{
    public class BaseResponse
    {
        public bool Success { get; set; } = false;

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

    public class BaseResponse<T> : BaseResponse
    {
        public BaseResponse(T data)
        {
            Data = data;
            Success = data != null;
        }

        public T Data { get; set; }
    }
}