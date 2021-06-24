using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiWithToken.Responses
{
    public class BaseResponse<T> where T : class
    {
        public T Entity { get; set; }
        public bool IsSuccess { get; set; }
        public string Message { get; set; }

        public BaseResponse(T _entity, bool _isSuccess, string _message)
        {
            Entity = _entity;
            IsSuccess = _isSuccess;
            Message = _message;
        }
    }
}
