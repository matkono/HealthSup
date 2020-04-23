using System.Collections.Generic;

namespace Cardiompp.Application.DataContracts.Responses
{
    public class BaseResponse
    {
        public bool Success { get; set; } = false;

        public IEnumerable<ErrorResponse> Errors { get; set; }
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