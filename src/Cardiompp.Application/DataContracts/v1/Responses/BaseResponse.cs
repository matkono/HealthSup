using System.Collections.Generic;
using System.Linq;

namespace Cardiompp.Application.DataContracts.Responses
{
    public class BaseResponse
    {
        public bool Success { get; set; } = false;

        public IEnumerable<ErrorResponse> Errors { get; set; }

        public void AddError(ErrorResponse error) 
        {
            if (Errors == null)
                Errors = new List<ErrorResponse>();

            var errorList = Errors.ToList();
            errorList.Add(error);
            Errors = errorList;
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